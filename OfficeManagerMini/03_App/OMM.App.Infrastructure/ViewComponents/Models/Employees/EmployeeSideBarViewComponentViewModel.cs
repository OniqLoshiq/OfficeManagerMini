using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Infrastructure.ViewComponents.Models.Employees
{
    public class EmployeeSideBarViewComponentViewModel : IMapFrom<EmployeeSideBarDto>
    {
        public string ProfilePicture { get; set; }

        public string EmployeeName { get; set; }

        public string Position { get; set; }
    }
}
