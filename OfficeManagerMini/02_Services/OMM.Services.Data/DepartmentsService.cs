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

        public IQueryable<T> GetAllDepartmentsByDto<T>()
        {
            return this.context.Departments.To<T>();
        }

        public string GetDepartmentNameById(int departmentId)
        {
            var name = this.context.Departments.SingleOrDefault(d => d.Id == departmentId)?.Name;

            //TODO:
            //if(name == null)
            //{

            //}

            return name;
        }
    }
}
