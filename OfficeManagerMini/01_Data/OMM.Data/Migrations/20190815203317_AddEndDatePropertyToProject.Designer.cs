﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OMM.Data;

namespace OMM.Data.Migrations
{
    [DbContext(typeof(OmmDbContext))]
    [Migration("20190815203317_AddEndDatePropertyToProject")]
    partial class AddEndDatePropertyToProject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OMM.Domain.Activity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("ReportId");

                    b.Property<int>("WorkingMinutes");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ReportId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("OMM.Domain.Asset", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssetTypeId");

                    b.Property<DateTime>("DateOfAquire");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("InventoryNumber");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<string>("ReferenceNumber");

                    b.HasKey("Id");

                    b.HasIndex("AssetTypeId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("InventoryNumber")
                        .IsUnique()
                        .HasFilter("[InventoryNumber] IS NOT NULL");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("OMM.Domain.AssetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AssetTypes");
                });

            modelBuilder.Entity("OMM.Domain.Assignment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignorId");

                    b.Property<DateTime?>("Deadline");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("ExecutorId");

                    b.Property<bool>("IsProjectRelated");

                    b.Property<string>("Name");

                    b.Property<int>("Priority");

                    b.Property<double>("Progress");

                    b.Property<string>("ProjectId");

                    b.Property<DateTime>("StartingDate");

                    b.Property<int>("StatusId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AssignorId");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("OMM.Domain.AssignmentsEmployees", b =>
                {
                    b.Property<string>("AssignmentId");

                    b.Property<string>("AssistantId");

                    b.HasKey("AssignmentId", "AssistantId");

                    b.HasIndex("AssistantId");

                    b.ToTable("AssignmentsEmployees");
                });

            modelBuilder.Entity("OMM.Domain.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignmentId");

                    b.Property<string>("CommentatorId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("CommentatorId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OMM.Domain.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("OMM.Domain.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("AccessLevel");

                    b.Property<DateTime>("AppointedOn");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<int?>("LeavingReasonId");

                    b.Property<DateTime?>("LeftOn");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PersonalPhoneNumber");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Position");

                    b.Property<string>("ProfilePicture");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("LeavingReasonId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("OMM.Domain.EmployeesProjectsPositions", b =>
                {
                    b.Property<string>("ProjectId");

                    b.Property<string>("EmployeeId");

                    b.Property<int>("ProjectPositionId");

                    b.HasKey("ProjectId", "EmployeeId", "ProjectPositionId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectPositionId");

                    b.ToTable("EmployeesProjectsRoles");
                });

            modelBuilder.Entity("OMM.Domain.LeavingReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Reason");

                    b.HasKey("Id");

                    b.ToTable("LeavingReasons");
                });

            modelBuilder.Entity("OMM.Domain.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Client");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("Deadline");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Name");

                    b.Property<int>("Priority");

                    b.Property<double>("Progress");

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("OMM.Domain.ProjectPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ProjectPositions");
                });

            modelBuilder.Entity("OMM.Domain.Report", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique()
                        .HasFilter("[ProjectId] IS NOT NULL");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("OMM.Domain.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OMM.Domain.Employee")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OMM.Domain.Employee")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OMM.Domain.Employee")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OMM.Domain.Employee")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OMM.Domain.Activity", b =>
                {
                    b.HasOne("OMM.Domain.Employee", "Employee")
                        .WithMany("Activities")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("OMM.Domain.Report", "Report")
                        .WithMany("Activities")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OMM.Domain.Asset", b =>
                {
                    b.HasOne("OMM.Domain.AssetType", "AssetType")
                        .WithMany("Assets")
                        .HasForeignKey("AssetTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OMM.Domain.Employee", "Employee")
                        .WithMany("Items")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("OMM.Domain.Assignment", b =>
                {
                    b.HasOne("OMM.Domain.Employee", "Assignor")
                        .WithMany("AssignedAssignments")
                        .HasForeignKey("AssignorId");

                    b.HasOne("OMM.Domain.Employee", "Executor")
                        .WithMany("ExecutionAssignments")
                        .HasForeignKey("ExecutorId");

                    b.HasOne("OMM.Domain.Project", "Project")
                        .WithMany("Assignments")
                        .HasForeignKey("ProjectId");

                    b.HasOne("OMM.Domain.Status", "Status")
                        .WithMany("Assignments")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OMM.Domain.AssignmentsEmployees", b =>
                {
                    b.HasOne("OMM.Domain.Assignment", "Assignment")
                        .WithMany("AssignmentsAssistants")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OMM.Domain.Employee", "Assistant")
                        .WithMany("AssistantToAssignments")
                        .HasForeignKey("AssistantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OMM.Domain.Comment", b =>
                {
                    b.HasOne("OMM.Domain.Assignment", "Assignment")
                        .WithMany("Comments")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OMM.Domain.Employee", "Commentator")
                        .WithMany("Comments")
                        .HasForeignKey("CommentatorId");
                });

            modelBuilder.Entity("OMM.Domain.Employee", b =>
                {
                    b.HasOne("OMM.Domain.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("OMM.Domain.LeavingReason", "LeavingReason")
                        .WithMany("Employees")
                        .HasForeignKey("LeavingReasonId");
                });

            modelBuilder.Entity("OMM.Domain.EmployeesProjectsPositions", b =>
                {
                    b.HasOne("OMM.Domain.Employee", "Employee")
                        .WithMany("ProjectsPositions")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OMM.Domain.Project", "Project")
                        .WithMany("Participants")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OMM.Domain.ProjectPosition", "ProjectPosition")
                        .WithMany("EmployeesProjects")
                        .HasForeignKey("ProjectPositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OMM.Domain.Project", b =>
                {
                    b.HasOne("OMM.Domain.Status", "Status")
                        .WithMany("Projects")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OMM.Domain.Report", b =>
                {
                    b.HasOne("OMM.Domain.Project", "Project")
                        .WithOne("Report")
                        .HasForeignKey("OMM.Domain.Report", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
