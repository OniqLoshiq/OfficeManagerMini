using OMM.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IAssignmentsEmployeesService
    {
        Task<bool> RemoveAssistantsAsync(List<string> assistantsToRemove, string assignmentId);

        Task<bool> AddAssistantsAsync(List<string> assistantsToAdd, string assignmentId);

        IEnumerable<AssignmentsEmployees> CreateWithAssistantsIds(List<string> assistantsIds);
    }
}
