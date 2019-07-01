using OMM.Domain;
using System.Linq;

namespace OMM.Data.Seeding
{
    public class StatusSeeder : ISeeder
    {
        private readonly OmmDbContext context;

        public StatusSeeder(OmmDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if (this.context.Statuses.Any())
            {
                return;
            }

            var statuses = new Status[]
            {
                new Status{ Name = "In Progress"},
                new Status{ Name = "Frozen"},
                new Status{ Name = "Waiting"},
                new Status{ Name = "Delayed"},
                new Status{ Name = "Completed"},
            };
            
            foreach (var status in statuses)
            {
                this.context.Statuses.Add(status);
            }

            this.context.SaveChanges();
        }
    }
}
