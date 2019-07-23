using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Departments;
using System.Collections.Generic;

namespace OMM.App.Infrastructure.ViewComponents.Models
{
    public class DepartmentEmployeesListViewComponentViewModel : IMapFrom<DepartmentEmployeesDto>
    {
        public string Name { get; set; }

        public IList<ActiveEmployeeDepartmentViewComponentViewModel> Employees { get; set; }
    }
}
