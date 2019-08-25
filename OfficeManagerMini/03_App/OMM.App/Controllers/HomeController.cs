using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Models;
using OMM.App.Models.ViewModels;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeesService employeesService;

        public HomeController(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUsername = this.User.Identity.Name;
            var vm = (await this.employeesService.GetEmployeeDtoByUsernameAsync<EmployeeIndexDto>(currentUsername).SingleOrDefaultAsync()).To<EmployeeIndexViewModel>();

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
