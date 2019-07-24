using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Employees;
using OMM.Services.SendGrid;
using System;
using System.Collections.Generic;
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

            if(departmentName == Constants.MANAGEMENT_DEPARTMENT)
            {
                roles.Add(Constants.MANAGEMENT_ROLE);
            }
            else if(departmentName == Constants.HR_DEPARTMENT)
            {
                roles.Add(Constants.HR_ROLE);
            }

            var roleResult = await this.userManger.AddToRolesAsync(employee, roles);

            return roleResult.Succeeded;
        }
    }
}
