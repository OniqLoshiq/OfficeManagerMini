using OMM.Services.Data.DTOs.ProjectPositions;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IProjectPositionsService
    {
        IQueryable<ProjectPostionDto> GetProjectPositions();

        Task<string> GetProjectPositionNameByIdAsync(int id);
    }
}
