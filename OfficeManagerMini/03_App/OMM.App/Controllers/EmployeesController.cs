using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Common;
using OMM.App.Models.InputModels;
using OMM.Domain;

namespace OMM.App.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly SignInManager<Employee> signInManager;

        public EmployeesController(SignInManager<Employee> signInManager)
        {
            this.signInManager = signInManager;
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
                var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, ErrorMessages.InvalidLogin);

            return this.View();
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

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