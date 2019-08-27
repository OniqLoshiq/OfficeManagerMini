using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.ProjectPositions;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class ProjectPositionsTests
    {
        private const string ProjectPosition_ProjectMananger_Name = "Project Manager";
        private const int ProjectPosition_ProjectManange_Id = 1;

        private const string ProjectPosition_Participant_Name = "Participant";
        private const int ProjectPosition_Participant_Id = 2;

        private const string ProjectPosition_Assistant_Name = "Assistant";
        private const int ProjectPosition_Assistant_Id = 3;

        private const int ProjectPosition_Invalid_Id1 = 0;
        private const int ProjectPosition_Invalid_Id2 = -1;
        private const int ProjectPosition_Invalid_Id3 = 20;


        private IProjectPositionsService projectPositionsService;

        private List<ProjectPosition> GetDummyData()
        {
            return new List<ProjectPosition>
            {
                new ProjectPosition{ Id = ProjectPosition_ProjectManange_Id, Name = ProjectPosition_ProjectMananger_Name},
                new ProjectPosition{ Id = ProjectPosition_Participant_Id, Name = ProjectPosition_Participant_Name},
                new ProjectPosition{ Id = ProjectPosition_Assistant_Id, Name = ProjectPosition_Assistant_Name},
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyData());
            await context.SaveChangesAsync();
        }

        public ProjectPositionsTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetProjectPositions_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "ProjectPositions GetProjectPositions() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.projectPositionsService = new ProjectPositionsService(context);

            List<ProjectPostionDto> actualResults = await this.projectPositionsService.GetProjectPositions().ToListAsync();
            List<ProjectPostionDto> expectedResults = GetDummyData().To<ProjectPostionDto>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            }
            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returned project positions is not correct");
        }

        [Fact]
        public async Task GetProjectPositions_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "ProjectPositions GetProjectPositions() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            this.projectPositionsService = new ProjectPositionsService(context);

            List<ProjectPostionDto> actualResults = await this.projectPositionsService.GetProjectPositions().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returned project positions is not correct");
        }

        [Theory]
        [InlineData(ProjectPosition_ProjectMananger_Name, ProjectPosition_ProjectManange_Id)]
        [InlineData(ProjectPosition_Participant_Name, ProjectPosition_Participant_Id)]
        [InlineData(ProjectPosition_Assistant_Name, ProjectPosition_Assistant_Id)]
        public async Task GetProjectPositionNameByIdAsync_ShouldReturnCorrectResult(string expectedName, int positionId)
        {
            string errorMessagePrefix = "ProjectPositions GetProjectPositionNameByIdAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.projectPositionsService = new ProjectPositionsService(context);

            var actualName = await this.projectPositionsService.GetProjectPositionNameByIdAsync(positionId);

            Assert.True(expectedName == actualName, errorMessagePrefix + " " + "The project position name is not correct");
        }

        [Theory]
        [InlineData(ProjectPosition_Invalid_Id1)]
        [InlineData(ProjectPosition_Invalid_Id2)]
        [InlineData(ProjectPosition_Invalid_Id3)]
        public async Task GetStatusNameByIdAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException(int id)
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.projectPositionsService = new ProjectPositionsService(context);

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => this.projectPositionsService.GetProjectPositionNameByIdAsync(id));

            Assert.Equal(string.Format(ErrorMessages.ProjectPositionInvalidRange, id), ex.Message);
        }
    }
}
