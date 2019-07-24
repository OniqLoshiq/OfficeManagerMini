using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Infrastructure.ViewComponents.Models.Employees;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class EmployeesAllActiveViewComponent : ViewComponent
    {
        private readonly IEmployeesService employeesService;

        public EmployeesAllActiveViewComponent(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activeEmployees = await this.employeesService.GetAllActiveEmployees().To<EmployeeActiveViewComponentViewModel>().ToListAsync();

            return View(activeEmployees);
        }
    }
}
