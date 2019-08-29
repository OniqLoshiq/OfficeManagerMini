using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Reports;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class ReportsServiceTests
    {
        private const string Report_Id_1 = "1";
        private const string Report_Id_2 = "2";

        private const string Project_Id_1 = "01";
        private const string Project_Id_2 = "02";

        private const string Project_Name_1 = "Project 1";
        private const string Project_Name_2 = "Project 2";

        private const string Activity_Id_1 = "001";
        private const string Activity_Id_2 = "002";
        private const string Activity_Id_3 = "003";
        private const string Activity_Id_4 = "004";
        private const string Activity_Id_5 = "005";

        private IReportsService reportsService;

        private List<Report> GetDummyData()
        {
            return new List<Report>
            {
                new Report
                {
                    Id = Report_Id_1,
                    ProjectId = Project_Id_1,
                    Project = new Project{ Id = Project_Id_1, Name = Project_Name_1 },
                    Activities = new List<Activity>
                    {
                        new Activity { Id = Activity_Id_1},
                        new Activity { Id = Activity_Id_2},
                        new Activity { Id = Activity_Id_3},
                    }
                },
                new Report
                {
                    Id = Report_Id_2,
                    ProjectId = Project_Id_2,
                    Project = new Project{ Id = Project_Id_2, Name = Project_Name_2 },
                    Activities = new List<Activity>
                    {
                        new Activity { Id = Activity_Id_4},
                        new Activity { Id = Activity_Id_5},
                    }
                },
            };
        }

        private List<Project> GetDummyProjectData()
        {
            return new List<Project>
            {
                new Project{ Id = Project_Id_1, Name = Project_Name_1 },
                new Project{ Id = Project_Id_2, Name = Project_Name_2 },
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyData());
            await context.SaveChangesAsync();
        }

        private async Task SeedProjectData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyProjectData());
            await context.SaveChangesAsync();
        }

        public ReportsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Theory]
        [InlineData(Report_Id_1)]
        [InlineData(Report_Id_2)]
        public async Task GetReportById_WithValidData_ShouldReturnCorrectResult(string id)
        {
            string errorMessagePrefix = "ActivitiesService EditActivityAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.reportsService = new ReportsService(context);

            ReportDetailsDto expectedResult = (await context.Reports.SingleAsync(a => a.Id == id)).To<ReportDetailsDto>();
            ReportDetailsDto actualResult = await this.reportsService.GetReportById<ReportDetailsDto>(id).SingleAsync();

            Assert.True(expectedResult.Project.Id == actualResult.Project.Id, errorMessagePrefix + " " + "Project id is not returned properly.");
            Assert.True(expectedResult.Project.Name == actualResult.Project.Name, errorMessagePrefix + " " + "Project name is not returned properly.");
            Assert.True(expectedResult.Activities.Count == actualResult.Activities.Count, errorMessagePrefix + " " + "Acivities are not returned properly.");
        }

        [Fact]
        public async Task GetReportById_WithInvalidId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.reportsService = new ReportsService(context);

            string invalidId = "Invalid id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.reportsService.GetReportById<ReportDetailsDto>(invalidId).SingleAsync());

            Assert.Equal(string.Format(ErrorMessages.ReportIdNullReference, invalidId), ex.Message);
        }

        [Theory]
        [InlineData(Project_Id_1)]
        [InlineData(Project_Id_2)]
        public async Task CreateReportAsync_WithValidData_ShouldCreateReportAndReturnTrue(string projectId)
        {
            string errorMessagePrefix = "ReportsService CreateReportAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedProjectData(context);
            this.reportsService = new ReportsService(context);

            bool actualResult = await this.reportsService.CreateReportAsync(projectId);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateReportAsync_WithInvalidProjectId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.reportsService = new ReportsService(context);

            string invalidId = "Invalid id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.reportsService.CreateReportAsync(invalidId));

            Assert.Equal(string.Format(ErrorMessages.ProjectIdNullReference, invalidId), ex.Message);
        }

        [Theory]
        [InlineData(Project_Id_1)]
        [InlineData(Project_Id_2)]
        public async Task CreateReportAsync_WithExistingProjectIdInReports_ShouldThrowArgumentException(string projectId)
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.reportsService = new ReportsService(context);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => this.reportsService.CreateReportAsync(projectId));

            Assert.Equal(string.Format(ErrorMessages.ReportInvalidProjectId, projectId), ex.Message);
        }
    }
}
