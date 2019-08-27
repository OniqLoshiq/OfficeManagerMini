using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Statuses;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
   public class StatusesServiceTests
    {
        private const string Status_InProgress_Name = "In Progress";
        private const int Status_InProgress_Id = 1;

        private const string Status_Frozen_Name = "Frozen";
        private const int Status_Frozen_Id = 2;

        private const string Status_Waiting_Name = "Waiting";
        private const int Status_Waiting_Id = 3;

        private const string Status_Delayed_Name = "Delayed";
        private const int Status_Delayed_Id = 4;

        private const string Status_Completed_Name = "Completed";
        private const int Status_Completed_Id = 5;

        private const string Invalid_Status_Name = "Invalid";
        private const int Invalid_Status_Id1 = 0;
        private const int Invalid_Status_Id2 = 10;


        private IStatusesService statusesService;

        private List<Status> GetDummyData()
        {
            return new List<Status>
            {
                new Status{ Id = Status_InProgress_Id, Name = Status_InProgress_Name},
                new Status{ Id = Status_Frozen_Id, Name =  Status_Frozen_Name},
                new Status{ Id = Status_Waiting_Id, Name = Status_Waiting_Name},
                new Status{ Id = Status_Delayed_Id, Name = Status_Delayed_Name},
                new Status{ Id = Status_Completed_Id, Name = Status_Completed_Name},
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyData());
            await context.SaveChangesAsync();
        }

        public StatusesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetAll_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "StatusesService GetAllStatuses() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.statusesService = new StatusesService(context);

            List<StatusListDto> actualResults = await this.statusesService.GetAllStatuses().ToListAsync();
            List<StatusListDto> expectedResults = GetDummyData().To<StatusListDto>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            }
            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returned statuses is not correct");
        }

        [Fact]
        public async Task GetAll_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "StatusesService GetAllStatuses() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            this.statusesService = new StatusesService(context);

            List<StatusListDto> actualResults = await this.statusesService.GetAllStatuses().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returned statuses is not correct");
        }

        [Theory]
        [InlineData(Status_InProgress_Name, Status_InProgress_Id)]
        [InlineData(Status_Frozen_Name, Status_Frozen_Id)]
        [InlineData(Status_Waiting_Name, Status_Waiting_Id)]
        [InlineData(Status_Delayed_Name, Status_Delayed_Id)]
        [InlineData(Status_Completed_Name, Status_Completed_Id)]
        public async Task GetStatusIdByNameAsync_ShouldReturnCorrectResult(string statusName, int expectedId)
        {
            string errorMessagePrefix = "StatusesService GetStatusIdByNameAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.statusesService = new StatusesService(context);

            var actualId = await this.statusesService.GetStatusIdByNameAsync(statusName);

            Assert.True(expectedId == actualId, errorMessagePrefix + " " + "The status id is not correct");
        }

        [Fact]
        public async Task GetStatusIdByNameAsync_WithNullName_ShouldThrowArgumentNullException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.statusesService = new StatusesService(context);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => this.statusesService.GetStatusIdByNameAsync(null));

            Assert.Equal(ErrorMessages.StatusNullParameter, ex.Message);
        }

        [Fact]
        public async Task GetStatusIdByNameAsync_WithInvalidName_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.statusesService = new StatusesService(context);

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.statusesService.GetStatusIdByNameAsync(Invalid_Status_Name));

            Assert.Equal(string.Format(ErrorMessages.StatusNameNullReference, Invalid_Status_Name), ex.Message);
        }

        [Theory]
        [InlineData(Status_InProgress_Name, Status_InProgress_Id)]
        [InlineData(Status_Frozen_Name, Status_Frozen_Id)]
        [InlineData(Status_Waiting_Name, Status_Waiting_Id)]
        [InlineData(Status_Delayed_Name, Status_Delayed_Id)]
        [InlineData(Status_Completed_Name, Status_Completed_Id)]
        public async Task GetStatusNameByIdAsync_ShouldReturnCorrectResult(string expectedName, int statusId)
        {
            string errorMessagePrefix = "StatusesService GetStatusIdByNameAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.statusesService = new StatusesService(context);

            var actualName = await this.statusesService.GetStatusNameByIdAsync(statusId);

            Assert.True(expectedName == actualName, errorMessagePrefix + " " + "The status name is not correct");
        }

        [Theory]
        [InlineData(Invalid_Status_Id1)]
        [InlineData(Invalid_Status_Id2)]
        public async Task GetStatusNameByIdAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException(int id)
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.statusesService = new StatusesService(context);

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => this.statusesService.GetStatusNameByIdAsync(id));

            Assert.Equal(string.Format(ErrorMessages.StatusInvalidRange, id), ex.Message);
        }
    }
}
