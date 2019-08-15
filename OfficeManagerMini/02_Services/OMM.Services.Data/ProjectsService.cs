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

        public ProjectsService(OmmDbContext context)
        {
            this.context = context;
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
            
            return result > 0;
        }
    }
}
