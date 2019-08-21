using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Projects;

namespace OMM.Services.Data
{
    public class ProjectsService : IProjectsService
    {
        private readonly OmmDbContext context;
        private readonly IReportsService reportsService;
        private readonly IStatusesService statusesService;
        private readonly IEmployeesService employeesService;
        private readonly IProjectPositionsService projectPositionsService;
        private readonly IEmployeesProjectsPositionsService employeesProjectsPositionsService;
        private readonly IAssignmentsService assignmentsService;

        public ProjectsService(OmmDbContext context, IReportsService reportsService, IStatusesService statusesService, 
            IEmployeesService employeesService, IProjectPositionsService projectPositionsService, 
            IEmployeesProjectsPositionsService employeesProjectsPositionsService, IAssignmentsService assignmentsService)
        {
            this.context = context;
            this.reportsService = reportsService;
            this.statusesService = statusesService;
            this.employeesService = employeesService;
            this.projectPositionsService = projectPositionsService;
            this.employeesProjectsPositionsService = employeesProjectsPositionsService;
            this.assignmentsService = assignmentsService;
        }
        public IQueryable<ProjectListDto> GetAllProjectsForList()
        {
            return this.context.Projects.To<ProjectListDto>();
        }

        public async Task<bool> CreateProjectAsync(ProjectCreateDto input)
        {
            var project = input.To<Project>();

            this.context.Projects.Add(project);
            var result = await this.context.SaveChangesAsync();

            var reportResult = await this.reportsService.CreateReportAsync(project.Id);

            return result > 0 && reportResult;
        }

        public IQueryable<ProjectAllListDto> GetAllProjects()
        {
            return this.context.Projects.To<ProjectAllListDto>();
        }

        public IQueryable<ProjectAllListDto> GetMyProjects(string employeeId)
        {
            return this.context.Projects.Where(p => p.Participants.Any(x => x.EmployeeId == employeeId)).To<ProjectAllListDto>();
        }

        public IQueryable<T> GetProjectById<T>(string projectId)
        {
            var project = this.context.Projects.Where(p => p.Id == projectId).To<T>();

            return project;
        }

        public async Task<bool> ChangeDataAsync(ProjectDetailsChangeDto input)
        {
            var project = await this.context.Projects.FirstOrDefaultAsync(p => p.Id == input.Id);
            var projectStatusName = await this.statusesService.GetStatusNameByIdAsync(project.StatusId);
            var inputStatusName = await this.statusesService.GetStatusNameByIdAsync(input.StatusId);

            if (input.Deadline != "-" && input.Deadline != null)
            {
                project.Deadline = DateTime.ParseExact(input.Deadline, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
            }
            else if(input.Deadline == null)
            {
                project.Deadline = null;
            }

            if ((input.EndDate != "-" && input.EndDate != null) && projectStatusName != Constants.STATUS_COMPLETED)
            {
                project.Progress = Constants.PROGRESS_MAX_VALUE;
                project.EndDate = DateTime.ParseExact(input.EndDate, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
                project.StatusId = await this.statusesService.GetStatusIdByNameAsync(Constants.STATUS_COMPLETED);
            }
            else if ((input.EndDate != "-" && input.EndDate != null) && projectStatusName == Constants.STATUS_COMPLETED)
            {
                if (inputStatusName != Constants.STATUS_COMPLETED)
                {
                    project.EndDate = null;
                    project.StatusId = input.StatusId;
                }

                project.Progress = input.Progress;
            }
            else if ((input.EndDate == "-" || input.EndDate == null) && projectStatusName == Constants.STATUS_COMPLETED)
            {
                project.EndDate = null;

                if (inputStatusName == Constants.STATUS_COMPLETED)
                {
                    project.StatusId = await this.statusesService.GetStatusIdByNameAsync(Constants.STATUS_INPROGRESS);
                }
                else
                {
                    project.StatusId = input.StatusId;
                }

                project.Progress = input.Progress;
            }
            else if ((input.EndDate == "-" || input.EndDate == null) && projectStatusName != Constants.STATUS_COMPLETED)
            {
                if (inputStatusName == Constants.STATUS_COMPLETED)
                {
                    project.EndDate = DateTime.UtcNow;
                    project.StatusId = input.StatusId;
                    project.Progress = Constants.PROGRESS_MAX_VALUE;
                }
                else
                {
                    project.Progress = input.Progress;
                    project.StatusId = input.StatusId;
                }
            }

            this.context.Projects.Update(project);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddParticipantAsync(ProjectParticipantDto input)
        {
            var participantToAdd = input.To<EmployeesProjectsPositions>();

            this.context.EmployeesProjectsRoles.Add(participantToAdd);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> CheckParticipantAsync(ProjectParticipantDto input)
        {
            var isParticipant = (await this.context.Projects
                .SingleOrDefaultAsync(p => p.Id == input.ProjectId))
                .Participants.Any(p => p.EmployeeId == input.EmployeeId);

            return isParticipant;
        }

        public async Task<bool> CheckIsParticipantLastManagerAsync(ProjectParticipantChangeDto input)
        {
            var projectParticipants = (await this.context.Projects.SingleOrDefaultAsync(p => p.Id == input.ProjectId))?.Participants;

            var participantToChange = projectParticipants.SingleOrDefault(p => p.EmployeeId == input.EmployeeId);

            var newProjectPosition = this.projectPositionsService.GetProjectPositionNameById(input.ProjectPositionId);

            if(participantToChange.ProjectPosition.Name == Constants.PROJECT_MANAGER_ROLE && newProjectPosition != Constants.PROJECT_MANAGER_ROLE)
            {
                var projectManagerRolesCount = projectParticipants.Where(p => p.ProjectPosition.Name == Constants.PROJECT_MANAGER_ROLE).Count();

                if(projectManagerRolesCount > 1)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public async Task<bool> IsEmployeeAuthorizedToChangeProject(string projectId, string currentUserId)
        {
            var projectParticipants = await this.context.Projects.Where(p => p.Id == projectId).Select(p => p.Participants).SingleOrDefaultAsync();
            var isCurrentUserParticipantIsProjectManager = projectParticipants.Any(p => p.EmployeeId == currentUserId && p.ProjectPosition.Name == Constants.PROJECT_MANAGER_ROLE);

            var isCurrentUserAdmin = await this.employeesService.CheckIfEmployeeIsInRole(currentUserId, Constants.ADMIN_ROLE);
            var isCurrentUserManagement = await this.employeesService.CheckIfEmployeeIsInRole(currentUserId, Constants.MANAGEMENT_ROLE);

            return isCurrentUserParticipantIsProjectManager || isCurrentUserAdmin || isCurrentUserManagement;
        }

        public async Task<bool> ChangeProjectPositionAsync(ProjectParticipantChangeDto participantToChange)
        {
            var projectParticipantRole = await this.context.EmployeesProjectsRoles.SingleOrDefaultAsync(p => p.ProjectId == participantToChange.ProjectId && p.EmployeeId == participantToChange.EmployeeId);

            projectParticipantRole.ProjectPositionId = participantToChange.ProjectPositionId;

            this.context.EmployeesProjectsRoles.Update(projectParticipantRole);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditProjectAsync(ProjectEditDto projectToEdit)
        {
            var project = await this.context.Projects.Where(p => p.Id == projectToEdit.Id).SingleOrDefaultAsync();

            project.Name = projectToEdit.Name;
            project.Client = projectToEdit.Client;
            project.Description = projectToEdit.Description;
            project.Priority = projectToEdit.Priority;

            project.CreatedOn = DateTime.ParseExact(projectToEdit.CreatedOn, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);

            if (projectToEdit.Deadline != "" && projectToEdit.Deadline != null)
            {
                project.Deadline = DateTime.ParseExact(projectToEdit.Deadline, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
            }

            #region ChangeStatusLogic 
            if (project.Status.Name == Constants.STATUS_COMPLETED && project.StatusId != projectToEdit.StatusId)
            {
                project.EndDate = null;
                project.StatusId = projectToEdit.StatusId;
                project.Progress = projectToEdit.Progress;
            }
            else if ((await this.statusesService.GetStatusNameByIdAsync(projectToEdit.StatusId)) == Constants.STATUS_COMPLETED && project.StatusId != projectToEdit.StatusId)
            {
                project.EndDate = DateTime.UtcNow;
                project.StatusId = projectToEdit.StatusId;
                project.Progress = Constants.PROGRESS_MAX_VALUE;
            }
            else
            {
                project.StatusId = projectToEdit.StatusId;
                project.Progress = projectToEdit.Progress;
            }
            #endregion  


            var participantsWithPositionsToRemove = project.Participants
                .Where(p => !projectToEdit.Participants
                        .Any(pp => p.EmployeeId == pp.EmployeeId && p.ProjectPositionId == pp.ProjectPositionId)).Select(p => p.To<ProjectEditParticipantDto>()).ToList();

            var participantsWithPositionsToAdd = projectToEdit.Participants
                .Where(pp => !project.Participants
                        .Any(p => pp.EmployeeId == p.EmployeeId && pp.ProjectPositionId == p.ProjectPositionId)).ToList();

            if (participantsWithPositionsToRemove.Count() > 0)
            {
                await this.employeesProjectsPositionsService.RemoveParticipantsAsync(participantsWithPositionsToRemove);
            }

            if (participantsWithPositionsToAdd.Count() > 0)
            {
                foreach (var participant in participantsWithPositionsToAdd)
                {
                    participant.ProjectId = project.Id;
                }

                await this.employeesProjectsPositionsService.AddParticipantsAsync(participantsWithPositionsToAdd);
            }

            this.context.Projects.Update(project);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteProjectAsync(string id)
        {
            var project = await this.context.Projects.Where(p => p.Id == id).SingleOrDefaultAsync();

            if(project.Assignments.Count > 0)
            {
                await this.assignmentsService.DeleteProjectAssignmentsAsync(project.Assignments.ToList());
            }

            this.context.Projects.Remove(project);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
