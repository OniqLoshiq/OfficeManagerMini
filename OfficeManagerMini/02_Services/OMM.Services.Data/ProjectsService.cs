using System.Linq;
using System.Threading.Tasks;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;

namespace OMM.Services.Data
{
    public class ProjectsService : IProjectsService
    {
        private readonly OmmDbContext context;
        private readonly IReportsService reportsService;

        public ProjectsService(OmmDbContext context, IReportsService reportsService)
        {
            this.context = context;
            this.reportsService = reportsService;
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
    }
}
