using OMM.Services.Data.DTOs.Projects;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IEmployeesProjectsPositionsService
    {
        Task<bool> ChangeEmployeeProjectPositionAsync(ProjectParticipantChangeDto participantToChange);
    }
}
