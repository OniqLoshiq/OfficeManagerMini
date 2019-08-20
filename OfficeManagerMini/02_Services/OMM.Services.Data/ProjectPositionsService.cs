using OMM.Data;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.ProjectPositions;
using System.Linq;

namespace OMM.Services.Data
{
    public class ProjectPositionsService : IProjectPositionsService
    {
        private readonly OmmDbContext context;

        public ProjectPositionsService(OmmDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ProjectPostionDto> GetProjectPositions()
        {
            return this.context.ProjectPositions.To<ProjectPostionDto>();
        }

        public string GetProjectPositionNameById(int id)
        {
            return this.context.ProjectPositions.SingleOrDefault(p => p.Id == id)?.Name;
        }
    }
}
