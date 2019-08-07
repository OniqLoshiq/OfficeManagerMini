using System.Linq;
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

        public IQueryable<AssignmentListDto> GetAllAssignments()
        {
            return this.context.Assignments.To<AssignmentListDto>();
        }

        public IQueryable<AssignmentListDto> GetAllMyAssignments(string employeeId)
        {
            var assignmentIds = this.context.Assignments
                .Where(a => a.ExecutorId == employeeId || a.AssignorId == employeeId)
                .Select(a => a.Id)
                .ToList();

            var assignmentAssistantIds = this.context.AssignmentsEmployees
                .Where(a => a.AssistantId == employeeId)
                .Select(a => a.AssignmentId)
                .ToList();

            var allAssignmentIds = assignmentIds.Union(assignmentAssistantIds);
            
            return this.context.Assignments
                .Where(a => allAssignmentIds.Contains(a.Id))
                .To<AssignmentListDto>();
        }

        public IQueryable<AssignmentListDto> GetAllAssignmentsForMe(string executorId)
        {
            return this.context.Assignments
                .Where(a => a.ExecutorId == executorId)
                .To<AssignmentListDto>();
        }

        public IQueryable<AssignmentListDto> GetAllAssignmentsFromMe(string assignorId)
        {
            return this.context.Assignments
                .Where(a => a.AssignorId == assignorId)
                .To<AssignmentListDto>();
        }

        public IQueryable<AssignmentListDto> GetAllAssignmentsAsAssistant(string assistantId)
        {
            return this.context.AssignmentsEmployees
                .Where(a => a.AssistantId == assistantId)
                .Select(a => a.Assignment)
                .To<AssignmentListDto>();
        }
    }
}
