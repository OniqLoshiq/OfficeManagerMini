using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;
using System.Collections.Generic;

namespace OMM.Services.Data.DTOs.Departments
{
    public class DepartmentEmployeesDto : IMapFrom<Department>
    {
        public string Name { get; set; }

        public IList<ActiveEmployeeDepartmentDto> Employees { get; set; }
    }
}
