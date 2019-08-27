using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;

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

        public async Task<string> GetDepartmentNameByIdAsync(int departmentId)
        {
            var name = (await this.context.Departments.SingleOrDefaultAsync(d => d.Id == departmentId))?.Name;

            if(name == null)
            {
                throw new ArgumentOutOfRangeException(null, string.Format(ErrorMessages.DepartmentInvalidRange, departmentId));
            }

            return name;
        }
    }
}
