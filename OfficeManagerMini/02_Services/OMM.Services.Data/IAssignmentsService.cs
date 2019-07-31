using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IAssignmentsService
    {
        Task<bool> CreateAssignment(string executorId);
    }
}
