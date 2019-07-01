using OMM.Domain;
using System.Linq;

namespace OMM.Data.Seeding
{
    public class DepartmentSeeder : ISeeder
    {
        private readonly OmmDbContext context;

        public DepartmentSeeder(OmmDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if(this.context.Departments.Any())
            {
                return;
            }

            var departments = new Department[]
            {
                new Department{ Name = "Management board"},
                new Department{ Name = "Administration"},
                new Department{ Name = "Accounting"},
                new Department{ Name = "Engineering"},
                new Department{ Name = "HR"},
            };

            foreach (var department in departments)
            {
                this.context.Departments.Add(department);
            }

            this.context.SaveChanges();
        }
    }
}
