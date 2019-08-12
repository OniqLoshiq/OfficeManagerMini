using System.Linq;
using OMM.Data;
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
    }
}
