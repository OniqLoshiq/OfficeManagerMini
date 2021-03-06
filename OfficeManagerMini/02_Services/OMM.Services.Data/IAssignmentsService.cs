﻿using OMM.Domain;
using OMM.Services.Data.DTOs.Assignments;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IAssignmentsService
    {
        Task<bool> CreateAssignmentAsync(AssignmentCreateDto input);

        IQueryable<AssignmentListDto> GetAllAssignments();

        IQueryable<AssignmentListDto> GetAllMyAssignments(string employeeId);

        IQueryable<AssignmentListDto> GetAllAssignmentsForMe(string executorId);

        IQueryable<AssignmentListDto> GetAllAssignmentsFromMe(string assignorId);

        IQueryable<AssignmentListDto> GetAllAssignmentsAsAssistant(string assistantId);

        IQueryable<AssignmentDetailsDto> GetAssignmentDetails(string id);

        Task<bool> ChangeDataAsync(AssignmentDetailsChangeDto input);

        Task<bool> DeleteAsync(string id);

        Task<bool> EditAsync(AssignmentEditDto input);

        Task<AssignmentEditDto> GetAssignmentToEditAsync(string id);

        Task<bool> DeleteProjectAssignmentsAsync(List<Assignment> assignmentsToDelete);
    }
}
