using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Activities;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class ActivitiesServiceTests
    {
        private const string Activity_Id_1 = "1";
        private const string Activity_Id_2 = "2";

        private const string Activity_Description_1 = "Some random activity description 1";
        private const string Activity_Description_2 = "Some random activity description 2";

        private const int Activity_WorkingMinutes_1 = 120;
        private const int Activity_WorkingMinutes_2 = 360;

        private const string Report_Id_1 = "01";
        private const string Report_Id_2 = "02";

        private const string Employee_Id_1 = "001";
        private const string Employee_Id_2 = "002";

        private const string Employee_FullName_1 = "Test employee 1";
        private const string Employee_FullName_2 = "Test employee 2";

        private IActivitiesService activitiesService;

        private List<Activity> GetDummyData()
        {
            return new List<Activity>
            {
                new Activity
                {
                    Id = Activity_Id_1,
                    Description = Activity_Description_1,
                    Date = DateTime.UtcNow.AddDays(-10),
                    WorkingMinutes = Activity_WorkingMinutes_1,
                    ReportId = Report_Id_1,
                    Report = new Report { Id = Report_Id_1},
                    EmployeeId = Employee_Id_1,
                    Employee = new Employee { Id = Employee_Id_1, FullName = Employee_FullName_1}
                },
                new Activity
                {
                    Id = Activity_Id_2,
                    Description = Activity_Description_2,
                    Date = DateTime.UtcNow.AddDays(-15),
                    WorkingMinutes = Activity_WorkingMinutes_2,
                    ReportId = Report_Id_2,
                    Report = new Report { Id = Report_Id_2},
                    EmployeeId = Employee_Id_2,
                    Employee = new Employee { Id = Employee_Id_2, FullName = Employee_FullName_2}
                }
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyData());
            await context.SaveChangesAsync();
        }

        public ActivitiesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateActivityAsync_WithValidData_ShouldCreateActivityAndReturnTrue()
        {
            string errorMessagePrefix = "ActivitiesService CreateActivityAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            var activityToCreate = new ActivityCreateDto
            {
                Description = "Create new activity",
                Date = "28/08/2019",
                WorkingTime = "04:12",
                EmployeeId = Employee_Id_1,
                ReportId = Report_Id_1
            };

            bool actualResult = await this.activitiesService.CreateActivityAsync(activityToCreate);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateActivityAsync_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            var activityToCreate = new ActivityCreateDto
            {
                Description = "Create new activity",
                Date = "28/08/2019",
                WorkingTime = "04:12",
                EmployeeId = "Invalid Id",
                ReportId = Report_Id_1
            };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.activitiesService.CreateActivityAsync(activityToCreate));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, activityToCreate.EmployeeId), ex.Message);
        }

        [Fact]
        public async Task CreateActivityAsync_WithInvalidReportId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            var activityToCreate = new ActivityCreateDto
            {
                Description = "Create new activity",
                Date = "28/08/2019",
                WorkingTime = "04:12",
                EmployeeId = Employee_Id_1,
                ReportId = "Invalid Id"
            };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.activitiesService.CreateActivityAsync(activityToCreate));

            Assert.Equal(string.Format(ErrorMessages.ReportIdNullReference, activityToCreate.ReportId), ex.Message);
        }

        [Theory]
        [InlineData(Activity_Id_1)]
        [InlineData(Activity_Id_2)]
        public async Task DeleteActivityAsync_WithValidData_ShouldDeleteActivityAndReturnTrue(string id)
        {
            string errorMessagePrefix = "ActivitiesService DeleteActivityAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            bool actualResult = await this.activitiesService.DeleteActivityAsync(id);
            var actualActivitiesCount = context.Activities.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.True(actualActivitiesCount == 1, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteActivityAsync_WithInvalidId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            string invalidId = "Invalid Id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.activitiesService.DeleteActivityAsync(invalidId));

            Assert.Equal(string.Format(ErrorMessages.ActivityIdNullReference, invalidId), ex.Message);
        }

        [Fact]
        public async Task EditActivityAsync_WithValidData_ShouldEditActivityAndReturnTrue()
        {
            string errorMessagePrefix = "ActivitiesService EditActivityAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            ActivityEditDto expectedResult = (await context.Activities.FirstAsync()).To<ActivityEditDto>();

            expectedResult.Date = "22/08/2019";
            expectedResult.Description = "New description when edited";
            expectedResult.WorkingTime = "01:33";

            await this.activitiesService.EditActivityAsync(expectedResult);

            var actualResult = (await context.Activities.FirstAsync()).To<ActivityEditDto>();

            Assert.True(expectedResult.Date == actualResult.Date, errorMessagePrefix + " " + "Date is not changed properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not changed properly.");
            Assert.True(expectedResult.WorkingTime == actualResult.WorkingTime, errorMessagePrefix + " " + "WorkingTime is not changed properly.");
        }

        [Fact]
        public async Task EditActivityAsync_WithInvalidId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            ActivityEditDto expectedResult = (await context.Activities.FirstAsync()).To<ActivityEditDto>();

            expectedResult.Date = "22/08/2019";
            expectedResult.Description = "New description when edited";
            expectedResult.WorkingTime = "01:33";
            expectedResult.Id = "Invalid Id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.activitiesService.EditActivityAsync(expectedResult));

            Assert.Equal(string.Format(ErrorMessages.ActivityIdNullReference, expectedResult.Id), ex.Message);
        }

        [Theory]
        [InlineData(Report_Id_1)]
        [InlineData(Report_Id_2)]
        public async Task GetActivitiesByReportId_WithValidData_ShouldReturnCorrectResult(string reportId)
        {
            string errorMessagePrefix = "ActivitiesService CreateActivityAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            List<ActivityPieDataDto> actualResults = await this.activitiesService.GetActivitiesByReportId(reportId).ToListAsync();
            List<ActivityPieDataDto> expectedResults = GetDummyData().Where(a => a.ReportId == reportId).To<ActivityPieDataDto>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.WorkingMinutes == actualEntry.WorkingMinutes, errorMessagePrefix + " " + "WorkingMinutes is not returned properly.");
                Assert.True(expectedEntry.EmployeeId == actualEntry.EmployeeId, errorMessagePrefix + " " + "EmployeeId is not returned properly.");
                Assert.True(expectedEntry.EmployeeFullName == actualEntry.EmployeeFullName, errorMessagePrefix + " " + "EmployeeFullName is not returned properly.");
            }
            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returned activities is not correct");
        }

        [Fact]
        public async Task GetActivitiesByReportId_WithZeroData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "ActivitiesService CreateActivityAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            List<ActivityPieDataDto> actualResults = await this.activitiesService.GetActivitiesByReportId(null).ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returned activities is not correct");
        }

        [Theory]
        [InlineData(Activity_Id_1)]
        [InlineData(Activity_Id_2)]
        public async Task GetActivityById_WithValidData_ShoultReturnCorrectResult(string id)
        {
            string errorMessagePrefix = "ActivitiesService EditActivityAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            ActivityEditDto expectedResult = (await context.Activities.SingleAsync(a => a.Id == id)).To<ActivityEditDto>();
            ActivityEditDto actualResult = await this.activitiesService.GetActivityById<ActivityEditDto>(id).SingleAsync();

            Assert.True(expectedResult.Date == actualResult.Date, errorMessagePrefix + " " + "Date is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not returned properly.");
            Assert.True(expectedResult.WorkingTime == actualResult.WorkingTime, errorMessagePrefix + " " + "WorkingTime is not returned properly.");
            Assert.True(expectedResult.ReportId == actualResult.ReportId, errorMessagePrefix + " " + "ReportId is not returned properly.");
        }

        [Fact]
        public async Task GetActivityById_WithInvalidId_ShoultReturnCorrectResult()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.activitiesService = new ActivitiesService(context);

            string invalidId = "Invalid id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.activitiesService.GetActivityById<ActivityEditDto>(invalidId).SingleAsync());

            Assert.Equal(string.Format(ErrorMessages.ActivityIdNullReference, invalidId), ex.Message);
        }
    }
}
