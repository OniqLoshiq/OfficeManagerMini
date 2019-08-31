using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OMM.App.Areas.Management.Models.InputModels;
using OMM.App.Areas.Management.Models.ViewModels;
using OMM.App.Common;
using OMM.App.Infrastructure.ViewComponents.Models.Employees;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Projects;
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
        private readonly IProjectsService projectsService;

        public ProjectsController(IEmployeesService employeesService, IProjectPositionsService projectPositionsService, IProjectsService projectsService)
        {
            this.employeesService = employeesService;
            this.projectPositionsService = projectPositionsService;
            this.projectsService = projectsService;
        }

        public async Task<IActionResult> Create()
        {
            //Little spaghetti code to load dynamically participants

            var vm = new ProjectCreateInputModel();

            var employeeSelectListInfo = await this.employeesService.GetActiveEmployeesWithDepartment().To<ActiveEmployeeDepartmentViewModel>().ToListAsync();

            List<string> departments = employeeSelectListInfo.Select(li => li.DepartmentName).Distinct().ToList();

            var departmentGroups = departments.Select(d => new SelectListGroup { Name = d });

            var participants = new List<SelectListItem>();

            foreach (var department in departmentGroups)
            {
                var employees = employeeSelectListInfo.Where(esli => esli.DepartmentName == department.Name).ToList();

                foreach (var employee in employees)
                {
                    participants.Add(
                    new SelectListItem
                    {
                        Value = employee.Id,
                        Text = employee.FullName,
                        Group = department
                    });
                }
            }

            ViewBag.Employees = participants;
            ViewBag.ProjectPositions = new SelectList(this.projectPositionsService.GetProjectPositions().ToList(), "Id", "Name");

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateInputModel input)
        {
            if(!ModelState.IsValid)
            {
                //Little spaghetti code to load dynamically participants

                var employeeSelectListInfo = await this.employeesService.GetActiveEmployeesWithDepartment().To<ActiveEmployeeDepartmentViewModel>().ToListAsync();

                List<string> departments = employeeSelectListInfo.Select(li => li.DepartmentName).Distinct().ToList();

                var departmentGroups = departments.Select(d => new SelectListGroup { Name = d });

                var participants = new List<SelectListItem>();

                foreach (var department in departmentGroups)
                {
                    var employees = employeeSelectListInfo.Where(esli => esli.DepartmentName == department.Name).ToList();

                    foreach (var employee in employees)
                    {
                        participants.Add(
                        new SelectListItem
                        {
                            Value = employee.Id,
                            Text = employee.FullName,
                            Group = department
                        });
                    }
                }


                ViewBag.Employees = participants;
                ViewBag.ProjectPositions = new SelectList(this.projectPositionsService.GetProjectPositions().ToList(), "Id", "Name");
                               
                return this.View(input);
            }

            var project = input.To<ProjectCreateDto>();

            await this.projectsService.CreateProjectAsync(project);

            return RedirectToAction("All");
        }

        public IActionResult GetEmployeesDepartmentViewComponent()
        {
            return ViewComponent("EmployeesDepartmentList", new { employeeId = ""});
        }

        public IActionResult GetProjectPositionsViewComponent()
        {
            return ViewComponent("ProjectPositionsList", new { projectPositionId = 0 });
        }

        public async Task<IActionResult> All()
        {
            var projects = new ProjectsAllViewModel();

            projects.AllOngoingProjects = await this.projectsService.GetAllProjects()
                    .Where(p => p.StatusName != Constants.STATUS_COMPLETED)
                    .OrderByDescending(p => p.StatusName == Constants.STATUS_INPROGRESS)
                    .ThenByDescending(p => p.Priority)
                    .To<ProjectsAllOngoingViewModel>()
                    .ToListAsync();

            projects.AllCompletedProjects = await this.projectsService.GetAllProjects()
                    .Where(p => p.StatusName == Constants.STATUS_COMPLETED)
                    .OrderByDescending(p => p.EndDate)
                    .ThenByDescending(p => p.Priority)
                    .To<ProjectsAllCompletedViewModel>()
                    .ToListAsync();


            return this.View(projects);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var projectToEdit = (await this.projectsService.GetProjectById<ProjectEditDto>(id).SingleOrDefaultAsync()).To<ProjectEditViewModel>();

            var employeeSelectListInfo = await this.employeesService.GetActiveEmployeesWithDepartment().To<ActiveEmployeeDepartmentViewModel>().ToListAsync();

            List<string> departments = employeeSelectListInfo.Select(li => li.DepartmentName).Distinct().ToList();

            var departmentGroups = departments.Select(d => new SelectListGroup { Name = d });

            var participants = new List<SelectListItem>();

            foreach (var department in departmentGroups)
            {
                var employees = employeeSelectListInfo.Where(esli => esli.DepartmentName == department.Name).ToList();

                foreach (var employee in employees)
                {
                    participants.Add(
                    new SelectListItem
                    {
                        Value = employee.Id,
                        Text = employee.FullName,
                        Group = department
                    });
                }
            }


            ViewBag.Employees = participants;
            ViewBag.ProjectPositions = new SelectList(this.projectPositionsService.GetProjectPositions().ToList(), "Id", "Name");

            return this.View(projectToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectEditViewModel input)
        {
            if(!ModelState.IsValid)
            {
                var employeeSelectListInfo = await this.employeesService.GetActiveEmployeesWithDepartment().To<ActiveEmployeeDepartmentViewModel>().ToListAsync();

                List<string> departments = employeeSelectListInfo.Select(li => li.DepartmentName).Distinct().ToList();

                var departmentGroups = departments.Select(d => new SelectListGroup { Name = d });

                var participants = new List<SelectListItem>();

                foreach (var department in departmentGroups)
                {
                    var employees = employeeSelectListInfo.Where(esli => esli.DepartmentName == department.Name).ToList();

                    foreach (var employee in employees)
                    {
                        participants.Add(
                        new SelectListItem
                        {
                            Value = employee.Id,
                            Text = employee.FullName,
                            Group = department
                        });
                    }
                }


                ViewBag.Employees = participants;
                ViewBag.ProjectPositions = new SelectList(this.projectPositionsService.GetProjectPositions().ToList(), "Id", "Name");

                return this.View(input);
            }

            var projectToEdit = input.To<ProjectEditDto>();

            await this.projectsService.EditProjectAsync(projectToEdit);

            return RedirectToAction("Details", "Projects", new { id = projectToEdit.Id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return RedirectToAction("All");
            }

            await this.projectsService.DeleteProjectAsync(id);

            return RedirectToAction("All");
        }
    }
}