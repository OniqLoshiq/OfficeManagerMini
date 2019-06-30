using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMM.Domain
{
    public class Activity
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int WorkingMinutes { get; set; }

        [ForeignKey(nameof(Employee))]
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey(nameof(Report))]
        public string ReportId { get; set; }
        public virtual Report Report { get; set; }
    }
}
