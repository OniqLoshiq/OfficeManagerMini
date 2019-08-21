using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Common;
using OMM.App.Models.InputModels;
using OMM.App.Models.ViewModels;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Projects;

namespace OMM.App.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectsService projectsService;
        private readonly IEmployeesService employeesService;
        private readonly IEmployeesProjectsPositionsService employeesProjectsPositionsService;
        private readonly IProjectPositionsService projectPositionsService;

        public ProjectsController(IProjectsService projectsService, IEmployeesService employeesService, IEmployeesProjectsPositionsService employeesProjectsPositionsService, IProjectPositionsService projectPositionsService)
        {
            this.projectsService = projectsService;
            this.employeesService = employeesService;
            this.employeesProjectsPositionsService = employeesProjectsPositionsService;
            this.projectPositionsService = projectPositionsService;
        }

        public async Task<IActionResult> MyProjects()
        {
            var myProjects = new ProjectsAlMylViewModel();
            var currentEmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            myProjects.AllMyOngoingProjects = await this.projectsService.GetMyProjects(currentEmployeeId)
               .Where(p => p.StatusName != Constants.STATUS_COMPLETED)
               .OrderByDescending(p => p.StatusName == Constants.STATUS_INPROGRESS)
               .ThenByDescending(p => p.Priority)
               .To<ProjectsAllMyOngoingViewModel>()
               .ToListAsync();

            myProjects.AllMyCompletedProjects = await this.projectsService.GetMyProjects(currentEmployeeId)
                .Where(p => p.StatusName == Constants.STATUS_COMPLETED)
                .OrderByDescending(p => p.EndDate)
                .ThenByDescending(p => p.Priority)
                .To<ProjectsAllMyCompletedViewModel>()
                .ToListAsync();

            return this.View(myProjects);
        }

        public async Task<IActionResult> Details(string id)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var IsEmployeeAuthorizedForProject = await this.projectsService.IsEmployeeAuthorizedForProject(id, currentUserId);

            if (!IsEmployeeAuthorizedForProject)
            {
                return Forbid();
            }

            var project = (await this.projectsService.GetProjectById<ProjectDetailsDto>(id).FirstOrDefaultAsync()).To<ProjectDetailsViewModel>();

            project.Participants = project.Participants
                .OrderByDescending(p => p.ProjectPositionName == Constants.PROJECT_MANAGER_ROLE)
                .ThenByDescending(p => p.ProjectPositionName == Constants.PROJECT_PARTICIPANT_ROLE)
                .ToList();

            ViewBag.isCurrentUserProjectManager = project.Participants.FirstOrDefault(p => p.ParticipantId == currentUserId)?.ProjectPositionName == Constants.PROJECT_MANAGER_ROLE;

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeData(ProjectDetailsChangeViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = input.Id });
            }

            var projectDataToChange = input.To<ProjectDetailsChangeDto>();

            await this.projectsService.ChangeDataAsync(projectDataToChange);

            return RedirectToAction("Details", new { id = input.Id });
        }

        public IActionResult AddParticipant(string id)
        {
            var model = new ProjectParticipantInputModel
            {
                ProjectId = id
            };

            return PartialView("_AddParticipantPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant(ProjectParticipantInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_AddParticipantPartial", input);
            }
            var participantToAdd = input.To<ProjectParticipantDto>();

            var checkIsEmployeeParticipant = await this.projectsService.CheckParticipantAsync(participantToAdd);

            if(checkIsEmployeeParticipant)
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.INVALID_PARTICIPANTS_DUPLICATE);

                return PartialView("_AddParticipantPartial", input);
            }

            await this.projectsService.AddParticipantAsync(participantToAdd);

            var redirectUrl = Url.Action("Details", "Projects", new { id = input.ProjectId });

            return Json(new { success = true, url = redirectUrl });
        }

        public async Task<IActionResult> ChangeProjectPosition([FromQuery]string projectId, [FromQuery]string participantId, [FromQuery]int projectPositionId)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var IsEmployeeAuthorizedToChangeProject = await this.projectsService.IsEmployeeAuthorizedToChangeProject(projectId, currentUserId);

            if(!IsEmployeeAuthorizedToChangeProject)
            {
                return Forbid();
            }

            var employeeFullName = await this.employeesService.GetEmployeeFullNameByIdAsync(participantId);

            var model = new ProjectParticipantChangeViewModel
            {
                ProjectId = projectId,
                EmployeeId = participantId,
                EmployeeFullName = employeeFullName,
                ProjectPositionId = projectPositionId
            };

            return PartialView("_ChangeProjectPositionPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProjectPosition(ProjectParticipantChangeViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ChangeProjectPositionPartial", input);
            }

            var participantToChange = input.To<ProjectParticipantChangeDto>();

            var checkIsEmployeeParticipantLastManager = await this.projectsService.CheckIsParticipantLastManagerAsync(participantToChange);

            if (checkIsEmployeeParticipantLastManager)
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.INVALID_PARTICIPANTS_MANAGER);

                return PartialView("_ChangeProjectPositionPartial", input);
            }

            await this.employeesProjectsPositionsService.ChangeEmployeeProjectPositionAsync(participantToChange);

            var newPositionName = this.projectPositionsService.GetProjectPositionNameById(participantToChange.ProjectPositionId);
            var participantFullName = participantToChange.EmployeeFullName;

            return Json(new { success = true, participantName = participantFullName, position = newPositionName });
        }

        public async Task<IActionResult> RemoveParticipant([FromQuery]string projectId, [FromQuery]string participantId, [FromQuery]int projectPositionId)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var IsEmployeeAuthorizedToChangeProject = await this.projectsService.IsEmployeeAuthorizedToChangeProject(projectId, currentUserId);

            if (!IsEmployeeAuthorizedToChangeProject)
            {
                return Forbid();
            }

            var employeeFullName = await this.employeesService.GetEmployeeFullNameByIdAsync(participantId);

            var model = new ProjectParticipantChangeViewModel
            {
                ProjectId = projectId,
                EmployeeId = participantId,
                EmployeeFullName = employeeFullName,
                ProjectPositionId = projectPositionId
            };

            return PartialView("_RemoveProjectParticipantPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveParticipant(ProjectParticipantChangeViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_RemoveProjectParticipantPartial", input);
            }

            var participantToRemove = input.To<ProjectParticipantChangeDto>();

            var checkIsEmployeeParticipantLastManager = await this.projectsService.CheckIsParticipantLastManagerAsync(participantToRemove);

            if (checkIsEmployeeParticipantLastManager)
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.INVALID_PARTICIPANTS_MANAGER);

                return PartialView("_RemoveProjectParticipantPartial", input);
            }

            await this.employeesProjectsPositionsService.RemoveParticipantAsync(participantToRemove);

            var participantFullName = participantToRemove.EmployeeFullName;

            return Json(new { success = true, participantName = participantFullName });
        }
    }
}