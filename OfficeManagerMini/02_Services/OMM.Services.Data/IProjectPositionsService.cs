using OMM.Services.Data.DTOs.ProjectPositions;
using System.Linq;

namespace OMM.Services.Data
{
    public interface IProjectPositionsService
    {
        IQueryable<ProjectPostionDto> GetProjectPositions();

        string GetProjectPositionNameById(int id);
    }
}
