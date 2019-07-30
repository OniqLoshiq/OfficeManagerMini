using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMM.Domain
{
    public class Employee : IdentityUser
    {
        public Employee()
        {
            this.Items = new HashSet<Asset>();
            this.AssignedAssignments = new HashSet<Assignment>();
            this.ExecutionAssignments = new HashSet<Assignment>();
            this.AssistantToAssignments = new HashSet<AssignmentsEmployees>();
            this.Comments = new HashSet<Comment>();
            this.Activities = new HashSet<Activity>();
            this.ProjectsPositions = new HashSet<EmployeesProjectsPositions>();
        }

        public string PersonalPhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime AppointedOn { get; set; }

        public DateTime? LeftOn { get; set; }

        public string Position { get; set; }

        public bool IsActive { get; set; }

        public string ProfilePicture { get; set; }

        public int AccessLevel { get; set; }
        

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey(nameof(LeavingReason))]
        public int? LeavingReasonId { get; set; }
        public virtual LeavingReason LeavingReason { get; set; }

        public virtual ICollection<Asset> Items { get; set; }

        [InverseProperty("Assignor")]
        public virtual ICollection<Assignment> AssignedAssignments { get; set; } 

        [InverseProperty("Executor")]
        public virtual ICollection<Assignment> ExecutionAssignments { get; set; } 

        public virtual ICollection<AssignmentsEmployees> AssistantToAssignments { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }

        public virtual ICollection<EmployeesProjectsPositions> ProjectsPositions { get; set; }
    }
}
