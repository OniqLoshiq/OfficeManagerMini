using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OMM.Domain;

namespace OMM.Data
{
    public class OmmDbContext : IdentityDbContext<Employee, IdentityRole, string>
    {
        public OmmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentsEmployees> AssignmentsEmployees { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeesProjectsPositions> EmployeesProjectsRoles { get; set; }
        public DbSet<LeavingReason> LeavingReasons { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ProjectPosition> ProjectPositions { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Project>()
                .HasOne(p => p.Report)
                .WithOne(r => r.Project)
                .HasForeignKey<Report>(r => r.ProjectId);

            builder.Entity<AssignmentsEmployees>()
                .HasKey(ae => new { ae.AssignmentId, ae.AssistantId });

            builder.Entity<EmployeesProjectsPositions>()
                .HasKey(epr => new { epr.ProjectId, epr.EmployeeId, epr.ProjectPositionId});

            builder.Entity<Asset>().HasIndex(a => a.InventoryNumber).IsUnique();

            builder.Entity<Comment>()
                .HasOne(c => c.Assignment)
                .WithMany(a => a.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Activity>()
                .HasOne(a => a.Report)
                .WithMany(r => r.Activities)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Report>()
                .HasOne(r => r.Project)
                .WithOne(p => p.Report)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EmployeesProjectsPositions>()
                .HasOne(epp => epp.Project)
                .WithMany(p => p.Participants)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

    }
}
