using Microsoft.AspNetCore.Identity;
using OMM.Domain;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OMM.Data;
using OMM.App.Infrastructure.Common;

namespace OMM.App.Infrastructure.AdminSeeder
{
    public class AdminSeeder
    {
        private const string AdminUsername = "root@admin.com";

        private const string AdminEmail = "root@admin.com";

        private const string AdminPassword = "12345";

        private const string AdminRole = "Admin";

        private const string AdminFirstName = "Root";

        private const string AdminLastName = "Admin";
        
        private const string AdminFullName = "Root App. Admin";
        
        private const string AdminPicture = "https://res.cloudinary.com/ommini/image/upload/v1563736712/Employees/admin_profile.jpg";
        
        private const int AdminAccessLevel = 10;

        private readonly IServiceProvider provider;

        public AdminSeeder(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public void Seed()
        {
            var context = this.provider.GetService<OmmDbContext>();

            var userManager = this.provider.GetService<UserManager<Employee>>();

            this.SeedAdminUser(context, userManager).GetAwaiter().GetResult();
        }

        private async Task SeedAdminUser(OmmDbContext context , UserManager<Employee> userManager)
        {
            if (!context.Users.Any(u => u.UserName == AdminUsername))
            {
                Employee admin = new Employee()
                {
                    UserName = AdminUsername,
                    Email = AdminEmail,
                    FirstName = AdminFirstName,
                    LastName = AdminLastName,
                    FullName = AdminFullName,
                    AccessLevel = AdminAccessLevel,
                    IsActive = true,
                    ProfilePicture = AdminPicture
                };

                var result = userManager.CreateAsync(admin, AdminPassword).GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, AdminRole);

                    await userManager.AddClaimAsync(admin, new Claim(InfrastructureConstants.ACCESS_LEVEL_CLAIM, admin.AccessLevel.ToString()));
                }
            }
        }
    }
}
