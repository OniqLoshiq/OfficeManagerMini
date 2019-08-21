using OMM.Services.Data.DTOs.Projects;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IProjectsService
    {
        IQueryable<ProjectListDto> GetAllProjectsForList();

        Task<bool> CreateProjectAsync(ProjectCreateDto input);

        IQueryable<ProjectAllListDto> GetAllProjects();

        IQueryable<ProjectAllListDto> GetMyProjects(string employeeId);

        IQueryable<T> GetProjectById<T>(string projectId);

        Task<bool> ChangeDataAsync(ProjectDetailsChangeDto input);

        Task<bool> AddParticipantAsync(ProjectParticipantDto input);

        Task<bool> CheckParticipantAsync(ProjectParticipantDto input);

        Task<bool> CheckIsParticipantLastManagerAsync(ProjectParticipantChangeDto input);

        Task<bool> IsEmployeeAuthorizedToChangeProject(string projectId, string currentUserId);

        Task<bool> ChangeProjectPositionAsync(ProjectParticipantChangeDto participantToChange);

        Task<bool> EditProjectAsync(ProjectEditDto projectToEdit);
    }
}
