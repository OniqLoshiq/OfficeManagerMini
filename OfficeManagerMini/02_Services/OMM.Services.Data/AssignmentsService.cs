using System.Threading.Tasks;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assignments;

namespace OMM.Services.Data
{
    public class AssignmentsService : IAssignmentsService
    {
        private readonly OmmDbContext context;

        public AssignmentsService(OmmDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateAssignmentAsync(AssignmentCreateDto input)
        {
            var assignment = input.To<Assignment>();

            foreach (var assistantId in input.AssistantsIds)
            {
                assignment.AssignmentsAssistants.Add(new AssignmentsEmployees
                {
                    AssistantId = assistantId,
                });
            }

            await this.context.Assignments.AddAsync(assignment);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
