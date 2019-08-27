using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.ProjectPositions;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<string> GetProjectPositionNameByIdAsync(int id)
        {
            var name = await this.context.ProjectPositions.Where(p => p.Id == id).Select(p => p.Name).SingleOrDefaultAsync();

            if(name == null)
            {
                throw new ArgumentOutOfRangeException(null, string.Format(ErrorMessages.ProjectPositionInvalidRange, id));
            }

            return name;
        }
    }
}
