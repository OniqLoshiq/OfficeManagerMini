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
    public class AssistantsAssignmentViewComponent : ViewComponent
    {
        private readonly IEmployeesService employeesService;

        public AssistantsAssignmentViewComponent(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<string> assistantsIds)
        {
            var vm = new AssistantAssignmentViewComponentViewModel();

            vm.AssistantsIds = assistantsIds;

            var employeeSelectListInfo = await this.employeesService.GetActiveEmployeesWithDepartment().To<ActiveEmployeeDepartmentViewModel>().ToListAsync();

            List<string> departments = employeeSelectListInfo.Select(li => li.DepartmentName).Distinct().ToList();

            var departmentGroups = departments.Select(d => new SelectListGroup { Name = d });

            foreach (var department in departmentGroups)
            {
                var employees = employeeSelectListInfo.Where(esli => esli.DepartmentName == department.Name).ToList();

                foreach (var employee in employees)
                {
                    vm.Assistants.Add(
                    new SelectListItem
                    {
                        Value = employee.Id,
                        Text = employee.FullName,
                        Group = department
                    });
                }
            }

            return View(vm);
        }
    }
}
