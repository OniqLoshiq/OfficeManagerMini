using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OMM.Data;
using OMM.Data.Seeding;
using System.Linq;
using System.Reflection;

namespace OMM.App.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDataBaseSeeding(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Assembly.GetAssembly(typeof(OmmDbContext))
                    .GetTypes()
                    .Where(t => typeof(ISeeder).IsAssignableFrom(t))
                    .Where(t => t.IsClass)
                    .Select(t => (ISeeder)serviceScope.ServiceProvider.GetRequiredService(t))
                    .ToList()
                    .ForEach(seeder => seeder.Seed());
            }
        }
    }
}
