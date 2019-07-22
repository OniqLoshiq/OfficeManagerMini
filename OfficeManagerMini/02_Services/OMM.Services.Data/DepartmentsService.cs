using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMM.Data;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Departments;

namespace OMM.Services.Data
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly OmmDbContext context;

        public DepartmentsService(OmmDbContext context)
        {
            this.context = context;
        }


        public IQueryable<DepartmentNameDto> GetAllDepartmentNames()
        {
            return this.context.Departments.To<DepartmentNameDto>();
        }

        public int GetDepartmentIdByName(string name)
        {
            var departmentId = this.context.Departments.SingleOrDefault(d => d.Name == name)?.Id;

            return departmentId ?? 0;
        }
    }
}
