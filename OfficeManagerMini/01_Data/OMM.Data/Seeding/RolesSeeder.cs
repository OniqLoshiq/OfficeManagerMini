using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace OMM.Data.Seeding
{
    public class RolesSeeder : ISeeder
    {
        private readonly OmmDbContext context;

        public RolesSeeder(OmmDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if (this.context.Roles.Any())
            {
                return;
            }

            var roles = new IdentityRole[]
            {
                new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole{ Name = "Management" , NormalizedName = "MANAGEMENT"},
                new IdentityRole{ Name = "HR" , NormalizedName = "HR"},
                new IdentityRole{ Name = "Employee" , NormalizedName = "EMPLOYEE"},
            };

            foreach (var role in roles)
            {
                this.context.Roles.Add(role);
            }

            this.context.SaveChanges();
        }
    }
}
