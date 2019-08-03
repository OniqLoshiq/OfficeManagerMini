using OMM.Services.Data.DTOs.Assignments;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IAssignmentsService
    {
        Task<bool> CreateAssignmentAsync(AssignmentCreateDto input);
    }
}
