using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OMM.App.Infrastructure.ViewComponents.Models.Employees;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class EmployeesDepartmentListViewComponent : ViewComponent
    {
        private readonly IEmployeesService employeesService;

        public EmployeesDepartmentListViewComponent(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string employeeId)
        {
            var vm = new EmployeesDepartmentListViewComponentViewModel
            {
                EmployeeId = employeeId
            };

            var employeeSelectListInfo =  await this.employeesService.GetActiveEmployeesWithDepartment().To<ActiveEmployeeDepartmentViewModel>().ToListAsync();

            List<string> departments = employeeSelectListInfo.Select(li => li.DepartmentName).Distinct().ToList();

            var departmentGroups = departments.Select(d => new SelectListGroup { Name = d });

            employeeSelectListInfo.ForEach(esli => 
                vm.Employees.Add(
                    new SelectListItem
                    {
                        Value = esli.Id,
                        Text = esli.FullName,
                        Group = departmentGroups.FirstOrDefault(d => d.Name == esli.DepartmentName)
                    }));

            return View(vm);
        }
    }
}
