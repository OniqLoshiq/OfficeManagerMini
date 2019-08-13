using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Employees
{
    public class EmployeeProjectParticipantAdditionalInfoDto : IMapFrom<Employee>
    {
        public string DepartmentName { get; set; }

        public string ProfilePicture { get; set; }

    }
}
