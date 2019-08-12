using OMM.Services.Data.DTOs.Projects;
using System.Linq;

namespace OMM.Services.Data
{
    public interface IProjectsService
    {
        IQueryable<ProjectListDto> GetAllProjectsForList();
    }
}
