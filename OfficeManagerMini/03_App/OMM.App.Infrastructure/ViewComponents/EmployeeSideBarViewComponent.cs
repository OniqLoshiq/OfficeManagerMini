using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Infrastructure.ViewComponents.Models.Employees;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Employees;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class EmployeeSideBarViewComponent : ViewComponent
    {
        private readonly IEmployeesService employeesService;

        public EmployeeSideBarViewComponent(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userName = this.User.Identity.Name;

            var vm = (await this.employeesService.GetEmployeeDtoByUsernameAsync<EmployeeSideBarDto>(userName).SingleOrDefaultAsync()).To<EmployeeSideBarViewComponentViewModel>();

            return this.View(vm);
        }
    }
}
