using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Common;
using OMM.App.Infrastructure.ViewComponents.Models.Employees;
using OMM.App.Models.InputModels;
using OMM.App.Models.ViewModels;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Departments;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService employeesService;
        private readonly IDepartmentsService departmentsService;

        public EmployeesController(IEmployeesService employeesService, IDepartmentsService departmentsService)
        {
            this.employeesService = employeesService;
            this.departmentsService = departmentsService;
        }

        public async Task<IActionResult> AllColleagues()
        {
            var departments = (await this.departmentsService
                .GetAllDepartmentsByDto<DepartmentEmployeesDto>()
                .ToListAsync())
                .Select(d => d.To<DepartmentEmployeesViewModel>())
                .ToList();

            return View(departments);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(EmployeeLoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                var loginModel = AutoMapper.Mapper.Map<EmployeeLoginDto>(model);

                if (await this.employeesService.LoginEmployeeAsync(loginModel))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, ErrorMessages.INVALID_LOGIN);

            return this.View();
        }

        public async Task<IActionResult> Logout()
        {
            await this.employeesService.LogoutEmployee();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string EmailToRetrieve)
        {
            bool validEmail = this.employeesService.IsEmailValid(EmailToRetrieve);

            if(validEmail)
            {
                await this.employeesService.RetrievePasswordAsync(EmailToRetrieve);
            }
            else
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.INVALID_EMAIL);
            }

            return this.RedirectToAction("Login");
        }

        public async Task<IActionResult> Profile()
        {
            var employeeId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var employeeProfile = (await this.employeesService
                .GetEmployeeDtoByIdAsync<EmployeeProfileDto>(employeeId)
                .SingleOrDefaultAsync())
                .To<EmployeeProfileViewModel>();

            return this.View(employeeProfile);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(EmployeeChangePasswordViewComponentViewModel input)
        {
            var employeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var employeeProfile = (await this.employeesService
                    .GetEmployeeDtoByIdAsync<EmployeeProfileDto>(employeeId)
                    .SingleOrDefaultAsync())
                    .To<EmployeeProfileViewModel>();

                return this.View("Profile", employeeProfile);
            }

            var isValidCurrentPassowrd = await this.employeesService.ValidateCurrentPasswordAsync(employeeId, input.CurrentPassword);

            if (!isValidCurrentPassowrd)
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.INVALID_PASSWORD);

                var employeeProfile = (await this.employeesService
                    .GetEmployeeDtoByIdAsync<EmployeeProfileDto>(employeeId)
                    .SingleOrDefaultAsync())
                    .To<EmployeeProfileViewModel>();

                return this.View("Profile", employeeProfile);
            }

            var employeeDto = input.To<EmployeeChangePasswordDto>();

            await this.employeesService.ChangePasswordAsync(employeeId, employeeDto);

            return this.RedirectToAction("Logout");
        }
    }
}