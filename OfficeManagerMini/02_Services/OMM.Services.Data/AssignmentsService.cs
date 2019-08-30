using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Assignments;

namespace OMM.Services.Data
{
    public class AssignmentsService : IAssignmentsService
    {
        private readonly OmmDbContext context;
        private readonly IStatusesService statusesService;
        private readonly IAssignmentsEmployeesService assignmentsEmployeesService;

        public AssignmentsService(OmmDbContext context, IStatusesService statusesService, IAssignmentsEmployeesService assignmentsEmployeesService)
        {
            this.context = context;
            this.statusesService = statusesService;
            this.assignmentsEmployeesService = assignmentsEmployeesService;
        }

        public async Task<bool> CreateAssignmentAsync(AssignmentCreateDto input)
        {
            var assignment = input.To<Assignment>();

            assignment.AssignmentsAssistants = this.assignmentsEmployeesService.CreateWithAssistantsIds(input.AssistantsIds).ToList();

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
            var isEmployeeIdValid = this.context.Users.Any(e => e.Id == employeeId);

            if(!isEmployeeIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.EmployeeIdNullReference, employeeId));
            }

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
            var isExecutorIdValid = this.context.Users.Any(e => e.Id == executorId);

            if (!isExecutorIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.EmployeeIdNullReference, executorId));
            }

            return this.context.Assignments
                .Where(a => a.ExecutorId == executorId)
                .To<AssignmentListDto>();
        }

        public IQueryable<AssignmentListDto> GetAllAssignmentsFromMe(string assignorId)
        {
            var isAssignorIdValid = this.context.Users.Any(e => e.Id == assignorId);

            if (!isAssignorIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.EmployeeIdNullReference, assignorId));
            }

            return this.context.Assignments
                .Where(a => a.AssignorId == assignorId)
                .To<AssignmentListDto>();
        }

        public IQueryable<AssignmentListDto> GetAllAssignmentsAsAssistant(string assistantId)
        {
            var isAssistantIdValid = this.context.Users.Any(e => e.Id == assistantId);

            if (!isAssistantIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.EmployeeIdNullReference, assistantId));
            }

            return this.context.AssignmentsEmployees
                .Where(a => a.AssistantId == assistantId)
                .Select(a => a.Assignment)
                .To<AssignmentListDto>();
        }

        public IQueryable<AssignmentDetailsDto> GetAssignmentDetails(string id)
        {
            var isAssignmentIdValid = this.context.Assignments.Any(a => a.Id == id);

            if(!isAssignmentIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.AssignmentIdNullReference, id));
            }

            return this.context.Assignments
                .Where(a => a.Id == id)
                .To<AssignmentDetailsDto>();
        }

        public async Task<bool> ChangeDataAsync(AssignmentDetailsChangeDto input)
        {
            var assignment = await this.context.Assignments.FirstOrDefaultAsync(a => a.Id == input.Id);

            if(assignment == null)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.AssignmentIdNullReference, input.Id));
            }

            var assignmentStatusName = await this.statusesService.GetStatusNameByIdAsync(assignment.StatusId);
            var inputStatusName = await this.statusesService.GetStatusNameByIdAsync(input.StatusId);

            if (input.Deadline != "-" && input.Deadline != null)
            {
                assignment.Deadline = DateTime.ParseExact(input.Deadline, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
            }
            else if (input.Deadline == null)
            {
                assignment.Deadline = null;
            }

            if ((input.EndDate != "-" && input.EndDate != null) && assignmentStatusName != Constants.STATUS_COMPLETED)
            {
                assignment.Progress = Constants.PROGRESS_MAX_VALUE;
                assignment.EndDate = DateTime.ParseExact(input.EndDate, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
                assignment.StatusId = await this.statusesService.GetStatusIdByNameAsync(Constants.STATUS_COMPLETED);
            }
            else if ((input.EndDate != "-" && input.EndDate != null) && assignmentStatusName == Constants.STATUS_COMPLETED)
            {
                if (inputStatusName != Constants.STATUS_COMPLETED)
                {
                    assignment.EndDate = null;
                    assignment.StatusId = input.StatusId;
                }

                assignment.Progress = input.Progress;
            }
            else if ((input.EndDate == "-" || input.EndDate == null) && assignmentStatusName == Constants.STATUS_COMPLETED)
            {
                assignment.EndDate = null;

                if (inputStatusName == Constants.STATUS_COMPLETED)
                {
                    assignment.StatusId = await this.statusesService.GetStatusIdByNameAsync(Constants.STATUS_INPROGRESS);
                }
                else
                {
                    assignment.StatusId = input.StatusId;
                }

                assignment.Progress = input.Progress;
            }
            else if ((input.EndDate == "-" || input.EndDate == null) && assignmentStatusName != Constants.STATUS_COMPLETED)
            {
                if (inputStatusName == Constants.STATUS_COMPLETED)
                {
                    assignment.EndDate = DateTime.UtcNow;
                    assignment.StatusId = input.StatusId;
                    assignment.Progress = Constants.PROGRESS_MAX_VALUE;
                }
                else
                {
                    assignment.Progress = input.Progress;
                    assignment.StatusId = input.StatusId;
                }
            }

            this.context.Assignments.Update(assignment);
            var result = await this.context.SaveChangesAsync();

            return result > 0;

        }

        public async Task<bool> DeleteAsync(string id)
        {
            var isAssignmentIdValid = this.context.Assignments.Any(a => a.Id == id);

            if (!isAssignmentIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.AssignmentIdNullReference, id));
            }

            var assignment = await this.context.Assignments.SingleOrDefaultAsync(a => a.Id == id);

            this.context.Assignments.Remove(assignment);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<AssignmentEditDto> GetAssignmentToEditAsync(string id)
        {
            var isAssignmentIdValid = this.context.Assignments.Any(a => a.Id == id);

            if (!isAssignmentIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.AssignmentIdNullReference, id));
            }

            return await this.context.Assignments
                .Where(a => a.Id == id)
                .To<AssignmentEditDto>().FirstOrDefaultAsync();
        }

        public async Task<bool> EditAsync(AssignmentEditDto input)
        {
            var assignment = await this.context.Assignments.SingleOrDefaultAsync(a => a.Id == input.Id);

            assignment.ExecutorId = input.ExecutorId;

            assignment.StartingDate = DateTime.ParseExact(input.StartingDate, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
                        
            if (input.Deadline == "")
            {
                assignment.Deadline = null;
            }
            else if (input.Deadline == null)
            {
                assignment.Deadline = null;
            }
            else
            {
                assignment.Deadline = DateTime.ParseExact(input.Deadline, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);
            }

            assignment.Priority = input.Priority;
            assignment.Type = input.Type;
            assignment.Name = input.Name;
            assignment.Description = input.Description;

            assignment.IsProjectRelated = input.IsProjectRelated;
            if (input.IsProjectRelated)
            {
                assignment.ProjectId = input.ProjectId;
            }
            else
            {
                assignment.ProjectId = null;
            }

            #region ChangeStatusLogic 
            if (assignment.Status.Name == Constants.STATUS_COMPLETED && assignment.StatusId != input.StatusId)
            {
                assignment.EndDate = null;
                assignment.StatusId = input.StatusId;
                assignment.Progress = input.Progress;
            }
            else if ((await this.statusesService.GetStatusNameByIdAsync(input.StatusId)) == Constants.STATUS_COMPLETED && assignment.StatusId != input.StatusId)
            {
                assignment.EndDate = DateTime.UtcNow;
                assignment.StatusId = input.StatusId;
                assignment.Progress = Constants.PROGRESS_MAX_VALUE;
            }
            else
            {
                assignment.StatusId = input.StatusId;
                assignment.Progress = input.Progress;
            }
            #endregion  

            var assignmentAssistantsIds = assignment.AssignmentsAssistants.Select(aa => aa.AssistantId).ToList();

            var assistantsToRemove = assignmentAssistantsIds.Except(input.AssistantsIds).ToList();

            var assistantsToAdd = input.AssistantsIds.Except(assignmentAssistantsIds).ToList();

            if (assistantsToRemove.Count() > 0)
            {
                await this.assignmentsEmployeesService.RemoveAssistantsAsync(assistantsToRemove, assignment.Id);
            }

            if (assistantsToAdd.Count() > 0)
            {
                await this.assignmentsEmployeesService.AddAssistantsAsync(assistantsToAdd, assignment.Id);
            }

            this.context.Assignments.Update(assignment);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteProjectAssignmentsAsync(List<Assignment> assignmentsToDelete)
        {
            this.context.Assignments.RemoveRange(assignmentsToDelete);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
