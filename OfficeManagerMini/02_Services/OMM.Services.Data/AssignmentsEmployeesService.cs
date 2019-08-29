using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.Data.Common;
using System;
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
            var isAssignmentIdValid = this.context.Assignments.Any(a => a.Id == assignmentId);

            if(!isAssignmentIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.AssignmentIdNullReference, assignmentId));
            }

            for (int i = 0; i < assistantsToAdd.Count; i++)
            {
                var isEmployeeIdValid = this.context.Users.Any(e => e.Id == assistantsToAdd[i]);

                if(!isEmployeeIdValid)
                {
                    throw new NullReferenceException(string.Format(ErrorMessages.EmployeeIdNullReference, assistantsToAdd[i]));
                }
            }

            var isEmployeeAlreadyAnAssistantInAssignment = await this.context.AssignmentsEmployees
                .AnyAsync(ae => ae.AssignmentId == assignmentId 
                                                   && assistantsToAdd.Contains(ae.AssistantId));

            if(isEmployeeAlreadyAnAssistantInAssignment)
            {
                throw new ArgumentException(ErrorMessages.AssistantArgumentException);
            }

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

        public IEnumerable<AssignmentsEmployees> CreateWithAssistantsIds(List<string> assistantsIds)
        {
            for (int i = 0; i < assistantsIds.Count; i++)
            {
                var isEmployeeIdValid = this.context.Users.Any(e => e.Id == assistantsIds[i]);

                if (!isEmployeeIdValid)
                {
                    throw new NullReferenceException(string.Format(ErrorMessages.EmployeeIdNullReference, assistantsIds[i]));
                }
            }

            var assignmentEmployees = assistantsIds.Select(id => new AssignmentsEmployees { AssistantId = id });

            return assignmentEmployees;
        }

        public async Task<bool> RemoveAssistantsAsync(List<string> assistantsToRemove, string assignmentId)
        {
            var isAssignmentIdValid = this.context.Assignments.Any(a => a.Id == assignmentId);

            if (!isAssignmentIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.AssignmentIdNullReference, assignmentId));
            }

            for (int i = 0; i < assistantsToRemove.Count; i++)
            {
                var isEmployeeIdValid = this.context.Users.Any(e => e.Id == assistantsToRemove[i]);

                if (!isEmployeeIdValid)
                {
                    throw new NullReferenceException(string.Format(ErrorMessages.EmployeeIdNullReference, assistantsToRemove[i]));
                }
            }

            var assistants = this.context.AssignmentsEmployees
                .Where(ae => ae.AssignmentId == assignmentId)
                .Where(ae => assistantsToRemove.Contains(ae.AssistantId));
                
            this.context.AssignmentsEmployees.RemoveRange(assistants);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
