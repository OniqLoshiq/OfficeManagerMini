using OMM.Domain;
using System.Linq;

namespace OMM.Data.Seeding
{
    public class ProjectPositionSeeder : ISeeder
    {
        private readonly OmmDbContext context;

        public ProjectPositionSeeder(OmmDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if (this.context.ProjectPositions.Any())
            {
                return;
            }
            
            var projectPositions = new ProjectPosition[]
            {
                new ProjectPosition{ Name = "Project Manager"},
                new ProjectPosition{ Name = "Participant"},
                new ProjectPosition{ Name = "Assistant"},
            };

            foreach (var projectPosition in projectPositions)
            {
                this.context.ProjectPositions.Add(projectPosition);
            }

            this.context.SaveChanges();
        }
    }
}
