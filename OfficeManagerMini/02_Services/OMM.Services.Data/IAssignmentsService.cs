using OMM.Services.Data.DTOs.Assignments;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IAssignmentsService
    {
        Task<bool> CreateAssignmentAsync(AssignmentCreateDto input);

        IQueryable<AssignmentListDto> GetAllAssignments();
    }
}
