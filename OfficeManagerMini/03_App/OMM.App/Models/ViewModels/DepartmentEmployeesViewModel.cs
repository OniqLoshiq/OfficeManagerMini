using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Departments;
using System.Collections.Generic;

namespace OMM.App.Models.ViewModels
{
    public class DepartmentEmployeesViewModel : IMapFrom<DepartmentEmployeesDto>
    {
        public string Name { get; set; }

        public List<EmployeeDepartmentViewModel> Employees { get; set; } = new List<EmployeeDepartmentViewModel>();
    }
}
