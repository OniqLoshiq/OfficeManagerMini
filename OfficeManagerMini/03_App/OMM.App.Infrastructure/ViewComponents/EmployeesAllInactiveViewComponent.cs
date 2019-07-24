using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Infrastructure.ViewComponents.Models.Employees;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class EmployeesAllInactiveViewComponent : ViewComponent
    {
        private readonly IEmployeesService employeesService;

        public EmployeesAllInactiveViewComponent(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var inactiveEmployees = await this.employeesService.GetAllInactiveEmployees().To<EmployeeInactiveViewComponentViewModel>().ToListAsync();

            return View(inactiveEmployees);
        }
    }
}
