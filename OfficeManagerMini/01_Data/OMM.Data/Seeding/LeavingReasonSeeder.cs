using OMM.Domain;
using System.Linq;

namespace OMM.Data.Seeding
{
    public class LeavingReasonSeeder : ISeeder
    {
        private readonly OmmDbContext context;

        public LeavingReasonSeeder(OmmDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if (this.context.LeavingReasons.Any())
            {
                return;
            }
            
            var leavingReasons = new LeavingReason[]
            {
                new LeavingReason{ Reason = "Retired"},
                new LeavingReason{ Reason = "Fired"},
                new LeavingReason{ Reason = "Resigned"},
            };

            foreach (var leavingReason in leavingReasons)
            {
                this.context.LeavingReasons.Add(leavingReason);
            }

            this.context.SaveChanges();
        }
    }
}
