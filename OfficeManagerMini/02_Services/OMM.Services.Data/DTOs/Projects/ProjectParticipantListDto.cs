using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectParticipantListDto : IMapTo<EmployeesProjectsPositions>
    {
        public string EmployeeId { get; set; }

        public int ProjectPositionId { get; set; }
    }
}
