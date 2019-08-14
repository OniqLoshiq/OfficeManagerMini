using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OMM.App.Areas.Management.Models.InputModels;
using OMM.App.Infrastructure.ViewComponents.Models.Employees;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.App.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin, Management")]
    public class ProjectsController : BaseController
    {
        private readonly IEmployeesService employeesService;
        private readonly IProjectPositionsService projectPositionsService;

        public ProjectsController(IEmployeesService employeesService, IProjectPositionsService projectPositionsService)
        {
            this.employeesService = employeesService;
            this.projectPositionsService = projectPositionsService;
        }

        public async Task<IActionResult> Create()
        {
            var vm = new ProjectCreateInputModel();

            var employeeSelectListInfo = await this.employeesService.GetActiveEmployeesWithDepartment().To<ActiveEmployeeDepartmentViewModel>().ToListAsync();

            List<string> departments = employeeSelectListInfo.Select(li => li.DepartmentName).Distinct().ToList();

            var departmentGroups = departments.Select(d => new SelectListGroup { Name = d });

            var participants = new List<SelectListItem>();

            employeeSelectListInfo.ForEach(esli =>
                participants.Add(
                    new SelectListItem
                    {
                        Value = esli.Id,
                        Text = esli.FullName,
                        Group = departmentGroups.FirstOrDefault(d => d.Name == esli.DepartmentName)
                    }));


            ViewBag.Employees = participants;
            ViewBag.ProjectPositions = new SelectList(this.projectPositionsService.GetProjectPositions().ToList(), "Id", "Name");

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateInputModel input)
        {
            if(!ModelState.IsValid)
            {
                var employeeSelectListInfo = await this.employeesService.GetActiveEmployeesWithDepartment().To<ActiveEmployeeDepartmentViewModel>().ToListAsync();

                List<string> departments = employeeSelectListInfo.Select(li => li.DepartmentName).Distinct().ToList();

                var departmentGroups = departments.Select(d => new SelectListGroup { Name = d });

                var participants = new List<SelectListItem>();

                employeeSelectListInfo.ForEach(esli =>
                    participants.Add(
                        new SelectListItem
                        {
                            Value = esli.Id,
                            Text = esli.FullName,
                            Group = departmentGroups.FirstOrDefault(d => d.Name == esli.DepartmentName)
                        }));


                ViewBag.Employees = participants;
                ViewBag.ProjectPositions = new SelectList(this.projectPositionsService.GetProjectPositions().ToList(), "Id", "Name");



                return this.View(input);
            }

            return Redirect("/");
        }

        public IActionResult GetEmployeesDepartmentViewComponent()
        {
            return ViewComponent("EmployeesDepartmentList", new { employeeId = ""});
        }

        public IActionResult GetProjectPositionsViewComponent()
        {
            return ViewComponent("ProjectPositionsList", new { projectPositionId = 0 });
        }
    }
}