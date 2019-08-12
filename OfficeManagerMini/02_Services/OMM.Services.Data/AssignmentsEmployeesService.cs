using OMM.Data;
using OMM.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public class AssignmentsEmployeesService : IAssignmentsEmployeesService
    {
        private readonly OmmDbContext context;

        public AssignmentsEmployeesService(OmmDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddAssistantsAsync(List<string> assistantsToAdd, string assignmentId)
        {
            var assistants = assistantsToAdd
                .Select(assistantId => new AssignmentsEmployees
                {
                    AssistantId = assistantId,
                    AssignmentId = assignmentId,
                });

            await this.context.AssignmentsEmployees.AddRangeAsync(assistants);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> RemoveAssistantsAsync(List<string> assistantsToRemove, string assignmentId)
        {
            var assistants = assistantsToRemove
                .Select(assistantId => new AssignmentsEmployees
                {
                    AssistantId = assistantId,
                    AssignmentId = assignmentId,
                });

            this.context.AssignmentsEmployees.RemoveRange(assistants);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
