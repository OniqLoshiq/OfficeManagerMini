using OMM.Services.Data.DTOs.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IEmployeesProjectsPositionsService
    {
        Task<bool> ChangeEmployeeProjectPositionAsync(ProjectParticipantChangeDto participantToChange);

        Task<bool> RemoveParticipantAsync(ProjectParticipantChangeDto participantToRemove);

        Task<bool> RemoveParticipantsAsync(List<ProjectEditParticipantDto> participantsToRemove);

        Task<bool> AddParticipantsAsync(List<ProjectEditParticipantDto> participantsToAdd);
    }
}
