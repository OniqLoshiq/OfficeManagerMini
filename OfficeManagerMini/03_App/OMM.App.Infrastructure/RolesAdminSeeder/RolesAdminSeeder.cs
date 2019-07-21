using Microsoft.AspNetCore.Identity;
using OMM.Domain;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OMM.Data;

namespace OMM.App.Infrastructure.RolesAdminSeeder
{
    public class RolesAdminSeeder
    {
        private readonly string[] Roles = { "Admin", "Management", "HR", "Employee" };
   
        private const string AdminUsername = "root@admin.com";

        private const string AdminEmail = "root@admin.com";

        private const string AdminPassword = "12345";

        private const int AdminAccessLevel = 10;

        private readonly IServiceProvider provider;

        public RolesAdminSeeder(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public void Seed()
        {
            var roleManager = this.provider.GetService<RoleManager<IdentityRole>>();

            this.SeedRoles(roleManager).GetAwaiter().GetResult();

            var context = this.provider.GetService<OmmDbContext>();

            var userManager = this.provider.GetService<UserManager<Employee>>();

            this.SeedFirstUser(context, userManager).GetAwaiter().GetResult();
        }

        private async Task SeedFirstUser(OmmDbContext context , UserManager<Employee> userManager)
        {
            if (!context.Users.Any())
            {
                Employee admin = new Employee()
                {
                    UserName = AdminUsername,
                    Email = AdminEmail,
                    AccessLevel = AdminAccessLevel,
                    IsActive = true
                };

                var result = userManager.CreateAsync(admin, AdminPassword).GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");

                    await userManager.AddClaimAsync(admin, new Claim("AccessLevel", admin.AccessLevel.ToString()));
                }
            }
        }

        private async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in Roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
