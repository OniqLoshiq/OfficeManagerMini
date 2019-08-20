using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectParticipantChangeDto : IMapTo<EmployeesProjectsPositions>
    {
        public string EmployeeId { get; set; }

        public string ProjectId { get; set; }

        public string EmployeeFullName { get; set; }

        public int ProjectPositionId { get; set; }
    }
}
