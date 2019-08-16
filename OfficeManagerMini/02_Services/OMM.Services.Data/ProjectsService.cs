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

        public ProjectsService(OmmDbContext context, IReportsService reportsService, IStatusesService statusesService)
        {
            this.context = context;
            this.reportsService = reportsService;
            this.statusesService = statusesService;
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

            if (input.EndDate != "-" && input.EndDate != null)
            {
                project.Progress = Constants.PROGRESS_MAX_VALUE;
                project.EndDate = DateTime.ParseExact(input.EndDate, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
                project.StatusId = await this.statusesService.GetStatusIdByNameAsync(Constants.STATUS_COMPLETED);
            }
            else if(input.EndDate == null)
            {
                project.EndDate = null;

                if(projectStatusName == Constants.STATUS_COMPLETED)
                {
                    if (inputStatusName == Constants.STATUS_COMPLETED)
                    {
                        project.StatusId = await this.statusesService.GetStatusIdByNameAsync(Constants.STATUS_INPROGRESS);
                    }
                    else
                    {
                        project.StatusId = input.StatusId;
                    }
                }
                else
                {
                    if (inputStatusName != Constants.STATUS_COMPLETED)
                    {
                        project.StatusId = input.StatusId;
                    }
                }

                project.Progress = input.Progress;
            }
            else if (inputStatusName == Constants.STATUS_COMPLETED)
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

            this.context.Projects.Update(project);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
