using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Areas.Management.Models.ViewModels
{
    public class EmployeeProjectParticipantAdditionalInfoViewModel : IMapFrom<EmployeeProjectParticipantAdditionalInfoDto>
    {
        public string DepartmentName { get; set; }

        public string ProfilePicture { get; set; }
    }
}
