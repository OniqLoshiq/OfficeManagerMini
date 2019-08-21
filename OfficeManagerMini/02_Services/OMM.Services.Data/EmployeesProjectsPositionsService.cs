using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;

namespace OMM.Services.Data
{
    public class EmployeesProjectsPositionsService : IEmployeesProjectsPositionsService
    {
        private readonly OmmDbContext context;

        public EmployeesProjectsPositionsService(OmmDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> ChangeEmployeeProjectPositionAsync(ProjectParticipantChangeDto participantToChange)
        {
            var projectParticipantRoleToDelete = await this.context.EmployeesProjectsRoles
                .Where(p => p.ProjectId == participantToChange.ProjectId 
                && p.EmployeeId == participantToChange.EmployeeId).SingleOrDefaultAsync();

            if(projectParticipantRoleToDelete.ProjectPositionId == participantToChange.ProjectPositionId)
            {
                return true;
            }

            var newProjectParticiapntRole = participantToChange.To<EmployeesProjectsPositions>();

            this.context.EmployeesProjectsRoles.Remove(projectParticipantRoleToDelete);
            var removeResult = await this.context.SaveChangesAsync();

            await this.context.EmployeesProjectsRoles.AddAsync(newProjectParticiapntRole);
            var resultAdd = await this.context.SaveChangesAsync();

            return resultAdd > 0 && removeResult > 0;
        }

        public async Task<bool> RemoveParticipantAsync(ProjectParticipantChangeDto participantToRemove)
        {
            var projectParticipantToDelete = await this.context.EmployeesProjectsRoles
                .Where(p => p.ProjectId == participantToRemove.ProjectId
                && p.EmployeeId == participantToRemove.EmployeeId).SingleOrDefaultAsync();

            this.context.EmployeesProjectsRoles.Remove(projectParticipantToDelete);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> RemoveParticipantsAsync(List<ProjectEditParticipantDto> participantsToRemove)
        {
            var projectParticipantsPositions = this.context.EmployeesProjectsRoles
                .Where(epr => participantsToRemove
                        .Any(pr => epr.ProjectId == pr.ProjectId && epr.EmployeeId == pr.EmployeeId && epr.ProjectPositionId == pr.ProjectPositionId));

            this.context.EmployeesProjectsRoles.RemoveRange(projectParticipantsPositions);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddParticipantsAsync(List<ProjectEditParticipantDto> participantsToAdd)
        {
            var projectParticipantsPositions = participantsToAdd.Select(p => p.To<EmployeesProjectsPositions>()).ToList();

            await this.context.EmployeesProjectsRoles.AddRangeAsync(projectParticipantsPositions);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
