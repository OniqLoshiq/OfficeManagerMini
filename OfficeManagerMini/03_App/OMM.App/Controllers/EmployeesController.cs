using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Common;
using OMM.App.Models.InputModels;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        public IActionResult All()
        {
            return View();
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

            ModelState.AddModelError(string.Empty, ErrorMessages.InvalidLogin);

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

        public IActionResult Profile()
        {
            return this.View();
        }
    }
}