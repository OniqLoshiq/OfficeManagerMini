﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Employees;
using OMM.Services.SendGrid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public class EmployeesService : IEmployeesService
    {
        private const string PASSWORD_ALLOWED_CHARS = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789_#$%@!^&*";
        private const int PASSWORD_MIN_LENGTH = 6;
        private const int PASSWORD_MAX_LENGTH = 14;
        private const int PASSWORD_ALLOWED_CHARS_START_INDEX = 0;

        private readonly UserManager<Employee> userManger;
        private readonly SignInManager<Employee> signInManager;
        private readonly OmmDbContext context;
        private readonly IDepartmentsService departmentsService;
        private readonly ISendGrid emailSender;

        public EmployeesService(UserManager<Employee> userManger, SignInManager<Employee> signInManager, OmmDbContext context, IDepartmentsService departmentsService, ISendGrid emailSender)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
            this.context = context;
            this.departmentsService = departmentsService;
            this.emailSender = emailSender;
        }

        public async Task<bool> LoginEmployeeAsync(EmployeeLoginDto employeeDto)
        {
            if (!this.IsEmployeeActive(employeeDto.Email))
            {
                return false;
            }

            var result = await this.signInManager.PasswordSignInAsync(employeeDto.Email, employeeDto.Password, employeeDto.RememberMe, false);

            return result.Succeeded;
        }

        public async Task LogoutEmployee()
        {
            await this.signInManager.SignOutAsync();

            return;
        }

        public async Task<bool> RegisterEmployeeAsync(EmployeeRegisterDto employeeRegisterDto)
        {
            var employee = Mapper.Map<Employee>(employeeRegisterDto);

            employee.IsActive = true;

            string password = this.GenerateEmployeePassword();

            var result = await this.userManger.CreateAsync(employee, password);

            var emailResult = await this.emailSender.SendRegistrationMailAsync(employee.Email, employee.FullName, password);

            //TODO:
            //if(!emailResult)
            //{
            //    throw new System.Exception("Problem with email");
            //}
            
            if (result.Succeeded)
            {
                if (await this.SignRolesToEmployee(employee, employeeRegisterDto.DepartmentId))
                {
                    await this.userManger.AddClaimAsync(employee, new Claim(Constants.ACCESS_LEVEL_CLAIM, employee.AccessLevel.ToString()));

                    return true;
                }
            }

            return false;
        }

        public IQueryable<ActiveEmployeeDepartmentDto> GetActiveEmployeesWithDepartment()
        {
            return this.context.Users.Where(u => u.IsActive && u.DepartmentId != null).To<ActiveEmployeeDepartmentDto>();
        }

        public IQueryable<EmployeeActiveDto> GetAllActiveEmployees()
        {
            var activeEmployees = this.context.Users.Where(u => u.IsActive && u.DepartmentId != null).To<EmployeeActiveDto>();

            return activeEmployees;
        }

        public IQueryable<EmployeeInactiveDto> GetAllInactiveEmployees()
        {
            var inactiveEmployees = this.context.Users.Where(u => !u.IsActive && u.DepartmentId != null).To<EmployeeInactiveDto>();

            return inactiveEmployees;
        }

        public IQueryable<T> GetEmployeeDtoByIdAsync<T>(string id)
        {
            var employee = this.context.Users.Where(u => u.Id == id).To<T>();

            return employee;
        }

        public IQueryable<T> GetEmployeeDtoByUsernameAsync<T>(string username)
        {
            var employee = this.context.Users.Where(u => u.UserName == username).To<T>();

            return employee;
        }
        
        public async Task<bool> EditAsync(EmployeeEditDto employeeToEdit)
        {
            var employee = await this.context.Users.FirstOrDefaultAsync(a => a.Id == employeeToEdit.Id);

            //TODO:
            //if(employee == null)
            //{
            //    throw new System.Exception();
            //}

            employee.UserName = employeeToEdit.Username;
            employee.Email = employeeToEdit.Email;
            employee.FirstName = employeeToEdit.FirstName;
            employee.MiddleName = employeeToEdit.MiddleName;
            employee.LastName = employeeToEdit.LastName;
            employee.FullName = employeeToEdit.FullName;
            employee.DateOfBirth = DateTime.ParseExact(employeeToEdit.DateOfBirth, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
            employee.PersonalPhoneNumber = employeeToEdit.PersonalPhoneNumber;

            if (employeeToEdit.ProfilePicture != null)
            {
                employee.ProfilePicture = employeeToEdit.ProfilePicture;
            }

            employee.Position = employeeToEdit.Position;

            if (employee.DepartmentId != employeeToEdit.DepartmentId)
            {
                await this.ChangeRolesAsync(employee, employeeToEdit.DepartmentId);

                employee.DepartmentId = employeeToEdit.DepartmentId;
            }

            employee.PhoneNumber = employeeToEdit.PhoneNumber;
            employee.AppointedOn = DateTime.ParseExact(employeeToEdit.AppointedOn, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);

            if (employee.AccessLevel != employeeToEdit.AccessLevel)
            {
                await this.ChangeAccessLevelClaimAsync(employee, employeeToEdit.AccessLevel);
            }

            this.context.Users.Update(employee);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ReleaseAsync(EmployeeReleaseDto employeeToRelease)
        {
            var employee = await this.context.Users.Where(u => u.Id == employeeToRelease.Id).SingleOrDefaultAsync();

            employee.IsActive = false;
            employee.LeavingReasonId = employeeToRelease.LeavingReasonId;
            employee.LeftOn = DateTime.ParseExact(employeeToRelease.LeftOn, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
            employee.PhoneNumber = null;
            employee.PersonalPhoneNumber = null;

            employee.Items.Clear();
            await this.ChangeAccessLevelClaimAsync(employee, 0);
            var employeeRoles = await this.userManger.GetRolesAsync(employee);
            await this.userManger.RemoveFromRolesAsync(employee, employeeRoles);

            this.ChangePictureToInactive(employee);

            this.context.Update(employee);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> HireBackAsync(EmployeeHireBackDto employeeToHireBack)
        {
            var employee = await this.context.Users.Where(u => u.Id == employeeToHireBack.Id).SingleOrDefaultAsync();

            employee.IsActive = true;
            employee.LeavingReasonId = null;
            employee.LeftOn = null;

            employee.Email = employeeToHireBack.Email;
            employee.UserName = employeeToHireBack.Username;
            employee.FirstName = employeeToHireBack.FirstName;
            employee.MiddleName = employeeToHireBack.MiddleName;
            employee.LastName = employeeToHireBack.LastName;
            employee.FullName = employeeToHireBack.FullName;
            employee.ProfilePicture = employeeToHireBack.ProfilePicture;
            employee.PersonalPhoneNumber = employeeToHireBack.PersonalPhoneNumber;
            employee.PhoneNumber = employeeToHireBack.PhoneNumber;
            employee.DepartmentId = employeeToHireBack.DepartmentId;
            employee.Position = employeeToHireBack.Position;
            employee.DateOfBirth = DateTime.ParseExact(employeeToHireBack.DateOfBirth, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
            employee.AppointedOn = DateTime.ParseExact(employeeToHireBack.AppointedOn, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);

            this.ChangePictureToActive(employee);

            await this.SignRolesToEmployee(employee, employeeToHireBack.DepartmentId);
            await this.ChangeAccessLevelClaimAsync(employee, employeeToHireBack.AccessLevel);

            string newPassword = this.GenerateEmployeePassword();

            await this.SetNewPassword(employee, newPassword);

            var emailResult = await this.emailSender.SendRegistrationMailAsync(employee.Email, employee.FullName, newPassword);

            this.context.Users.Update(employee);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ValidateCurrentPasswordAsync(string employeeId, string currentPassword)
        {
            var employee = await this.context.Users.SingleOrDefaultAsync(u => u.Id == employeeId);

            return await this.userManger.CheckPasswordAsync(employee, currentPassword);
        }

        public async Task<bool> ChangePasswordAsync(string employeeId, EmployeeChangePasswordDto changePasswordDto)
        {
            var employee = await this.context.Users.SingleOrDefaultAsync(u => u.Id == employeeId);

            var result = await this.userManger.ChangePasswordAsync(employee, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            return result.Succeeded;
        }

        public async Task<bool> RetrievePasswordAsync(string email)
        {
            var newPassword = this.GenerateEmployeePassword();

            var employee = await this.context.Users.SingleOrDefaultAsync(u => u.Email == email);

            await this.userManger.RemovePasswordAsync(employee);

            var result = await this.userManger.AddPasswordAsync(employee, newPassword);

            await this.emailSender.SendRegistrationMailAsync(employee.Email, employee.FullName, newPassword);

            return result.Succeeded;
        }

        public bool IsEmailValid (string email)
        {
            var isMailValid = this.IsEmployeeActive(email);

            return isMailValid;
        }

        public async Task<string> GetEmployeeFullNameByIdAsync(string currentEmployeeId)
        {
            var fullName = (await this.context.Users.SingleOrDefaultAsync(u => u.Id == currentEmployeeId)).FullName;

            return fullName;
        }

        public async Task<bool> CheckIfEmployeeIsInRole(string currentUserId, string roleName)
        {
            var employee = await this.context.Users.SingleOrDefaultAsync(e => e.Id == currentUserId);

            var result =  await this.userManger.IsInRoleAsync(employee, roleName);

            return result;
        }

        //Helper methods

        private async Task SetNewPassword(Employee employee, string newPassword)
        {
            await this.userManger.RemovePasswordAsync(employee);

            await this.userManger.AddPasswordAsync(employee, newPassword);
        }

        private void ChangePictureToInactive(Employee employee)
        {
            int prefixIndex = employee.ProfilePicture.IndexOf(Constants.PICTURE_SEARCH_PREFIX);

            employee.ProfilePicture = employee.ProfilePicture.Insert(prefixIndex + Constants.PICTURE_PREFIX_LENGHT, Constants.PICTURE_INACTIVE_ADDON);
        }

        private void ChangePictureToActive(Employee employee)
        {
            if (employee.ProfilePicture.Contains(Constants.PICTURE_INACTIVE_ADDON))
            {
                employee.ProfilePicture = employee.ProfilePicture.Replace(Constants.PICTURE_INACTIVE_ADDON, string.Empty);
            }
        }

        private async Task ChangeAccessLevelClaimAsync(Employee employee, int accessLevel)
        {
            var claim = this.userManger.GetClaimsAsync(employee).GetAwaiter().GetResult().Single(c => c.Type == Constants.ACCESS_LEVEL_CLAIM);

            await this.userManger.ReplaceClaimAsync(employee, claim, new Claim(Constants.ACCESS_LEVEL_CLAIM, accessLevel.ToString()));

            employee.AccessLevel = accessLevel;
        }

        private async Task ChangeRolesAsync(Employee employee, int departmentId)
        {
            var departmentName = this.departmentsService.GetDepartmentNameById(departmentId);

            var rolesCount = this.userManger.GetRolesAsync(employee).GetAwaiter().GetResult().Count;

            if (rolesCount > 1)
            {
                var isInManagerRole = this.userManger.IsInRoleAsync(employee, Constants.MANAGEMENT_ROLE).GetAwaiter().GetResult();
                _ = isInManagerRole ?
                                        await this.userManger.RemoveFromRoleAsync(employee, Constants.MANAGEMENT_ROLE) :
                                        await this.userManger.RemoveFromRoleAsync(employee, Constants.HR_ROLE);
            }

            if (departmentName == Constants.MANAGEMENT_DEPARTMENT)
            {
                await this.userManger.AddToRoleAsync(employee, Constants.MANAGEMENT_ROLE);
            }
            else if (departmentName == Constants.HR_DEPARTMENT)
            {
                await this.userManger.AddToRoleAsync(employee, Constants.HR_ROLE);
            }
        }

        private bool IsEmployeeActive(string email)
        {
            return this.context.Users.Any(u => u.Email == email && u.IsActive == true);
        }

        private string GenerateEmployeePassword()
        {
            Random rnd1 = new Random();
            int passwordLength = rnd1.Next(PASSWORD_MIN_LENGTH, PASSWORD_MAX_LENGTH);

            char[] chars = new char[passwordLength];
            Random rnd2 = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = PASSWORD_ALLOWED_CHARS[rnd2.Next(PASSWORD_ALLOWED_CHARS_START_INDEX, PASSWORD_ALLOWED_CHARS.Length)];
            }

            return new string(chars);
        }

        private async Task<bool> SignRolesToEmployee(Employee employee, int departmentId)
        {
            var roles = new List<string>();

            roles.Add(Constants.DEFAULT_ROLE);

            var departmentName = this.departmentsService.GetDepartmentNameById(departmentId);

            //TODO:
            //if(departmentName == null)
            //{

            //}

            if (departmentName == Constants.MANAGEMENT_DEPARTMENT)
            {
                roles.Add(Constants.MANAGEMENT_ROLE);
            }
            else if (departmentName == Constants.HR_DEPARTMENT)
            {
                roles.Add(Constants.HR_ROLE);
            }

            var roleResult = await this.userManger.AddToRolesAsync(employee, roles);

            return roleResult.Succeeded;
        }
    }
}
