using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMM.Domain
{
    public class Report
    {
        public Report()
        {
            this.Activities = new HashSet<Activity>();
        }

        public string Id { get; set; }

        public string ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
