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
    }
}
