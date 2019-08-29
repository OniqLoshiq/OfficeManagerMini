using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Projects;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class EmployeesProjectsPositionsServiceTests
    {
        private const string Employee_Id_1 = "001";
        private const string Employee_Id_2 = "002";
        private const string Employee_Id_3 = "003";
        private const string Employee_Id_4 = "004";
        private const string Employee_Id_5 = "005";

        private const string Employee_FullName_1 = "Test employee 1";
        private const string Employee_FullName_2 = "Test employee 2";
        private const string Employee_FullName_3 = "Test employee 3";
        private const string Employee_FullName_4 = "Test employee 4";
        private const string Employee_FullName_5 = "Test employee 5";

        private const string Employee_ProfilePicture_1 = "some link 1";
        private const string Employee_ProfilePicture_2 = "some link 2";
        private const string Employee_ProfilePicture_3 = "some link 3";
        private const string Employee_ProfilePicture_4 = "some link 3";
        private const string Employee_ProfilePicture_5 = "some link 3";

        private const string Department_Name_1 = "Management board";
        private const string Department_Name_2 = "Engineering";
        private const string Department_Name_3 = "HR";
        private const string Department_Name_4 = "Administration";
        private const string Department_Name_5 = "Accounting";

        private const string Project_Id_1 = "01";
        private const string Project_Id_2 = "02";

        private const string Project_Name_1 = "Project 1";
        private const string Project_Name_2 = "Project 2";

        private const int ProjectPosition_Id_1 = 1;
        private const int ProjectPosition_Id_2 = 2;
        private const int ProjectPosition_Id_3 = 3;

        private const string ProjectPosition_Name_1 = "Project manager";
        private const string ProjectPosition_Name_2 = "Participant";
        private const string ProjectPosition_Name_3 = "Assistant";

        private IEmployeesProjectsPositionsService employeesProjectsPositionsService;

        private List<EmployeesProjectsPositions> GetEmployeeProjectsPositionsDummyData()
        {
            return new List<EmployeesProjectsPositions>
            {
                new EmployeesProjectsPositions { EmployeeId = Employee_Id_1, ProjectId = Project_Id_1, ProjectPositionId = ProjectPosition_Id_1},
                new EmployeesProjectsPositions { EmployeeId = Employee_Id_2, ProjectId = Project_Id_1, ProjectPositionId = ProjectPosition_Id_3},
                new EmployeesProjectsPositions { EmployeeId = Employee_Id_3, ProjectId = Project_Id_2, ProjectPositionId = ProjectPosition_Id_2},

            };
        }

        private List<Employee> GetEmployeeDummyData()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = Employee_Id_1,
                    FullName = Employee_FullName_1,
                    ProfilePicture = Employee_ProfilePicture_1,
                    Department = new Department { Name = Department_Name_1} 
                },
               new Employee
                {
                    Id = Employee_Id_2,
                    FullName = Employee_FullName_2,
                    ProfilePicture = Employee_ProfilePicture_2,
                    Department = new Department { Name = Department_Name_2}
                },
               new Employee
                {
                    Id = Employee_Id_3,
                    FullName = Employee_FullName_3,
                    ProfilePicture = Employee_ProfilePicture_3,
                    Department = new Department { Name = Department_Name_3}
                },
               new Employee
                {
                    Id = Employee_Id_4,
                    FullName = Employee_FullName_4,
                    ProfilePicture = Employee_ProfilePicture_4,
                    Department = new Department { Name = Department_Name_4}
                },
               new Employee
                {
                    Id = Employee_Id_5,
                    FullName = Employee_FullName_5,
                    ProfilePicture = Employee_ProfilePicture_5,
                    Department = new Department { Name = Department_Name_5}
                },
            };
        }

        private List<Project> GetProjectDummyData()
        {
            return new List<Project>
            {
                new Project{Id = Project_Id_1,Name = Project_Name_1,},
                new Project{Id = Project_Id_2,Name = Project_Name_2,},
            };
        }

        private List<ProjectPosition> GetProjectPositionDummyData()
        {
            return new List<ProjectPosition>
            {
                new ProjectPosition{Id = ProjectPosition_Id_1,Name = ProjectPosition_Name_1,},
                new ProjectPosition{Id = ProjectPosition_Id_2,Name = ProjectPosition_Name_2,},
                new ProjectPosition{Id = ProjectPosition_Id_3,Name = ProjectPosition_Name_3,},
            };
        }


        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetEmployeeDummyData());
            await context.AddRangeAsync(GetProjectDummyData());
            await context.AddRangeAsync(GetProjectPositionDummyData());
            await context.SaveChangesAsync();

            await context.AddRangeAsync(GetEmployeeProjectsPositionsDummyData());
            await context.SaveChangesAsync();
        }

        public EmployeesProjectsPositionsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task ChangeEmployeeProjectPositionAsync_WithValidData_ShouldChangeProjectPositionAndReturnTrue()
        {
            string errorMessagePrefix = "EmployeesProjectsPositions ChangeEmployeeProjectPositionAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            ProjectParticipantChangeDto expectedResult = GetEmployeeProjectsPositionsDummyData().First().To<ProjectParticipantChangeDto>();
            expectedResult.ProjectPositionId = ProjectPosition_Id_2;

            bool returnResult = await this.employeesProjectsPositionsService.ChangeEmployeeProjectPositionAsync(expectedResult);

            ProjectParticipantChangeDto actualResult = (await context.EmployeesProjectsRoles.FirstAsync()).To<ProjectParticipantChangeDto>();

            Assert.True(expectedResult.EmployeeId == actualResult.EmployeeId, errorMessagePrefix + " " + "EmployeeId is not returned properly.");
            Assert.True(expectedResult.ProjectId == actualResult.ProjectId, errorMessagePrefix + " " + "ProjectId is not returned properly.");
            Assert.True(expectedResult.ProjectPositionId == actualResult.ProjectPositionId, errorMessagePrefix + " " + "ProjectPositionId is not changed properly.");

            Assert.True(returnResult, errorMessagePrefix);
        }

        [Fact]
        public async Task ChangeEmployeeProjectPositionAsync_WithInvalidProjectId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            ProjectParticipantChangeDto expectedResult = GetEmployeeProjectsPositionsDummyData().First().To<ProjectParticipantChangeDto>();
            expectedResult.ProjectPositionId = ProjectPosition_Id_2;
            expectedResult.ProjectId = "InvalidId";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesProjectsPositionsService.ChangeEmployeeProjectPositionAsync(expectedResult));

            Assert.Equal(ErrorMessages.ProjectParticipantNullReference, ex.Message);
        }

        [Fact]
        public async Task ChangeEmployeeProjectPositionAsync_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            ProjectParticipantChangeDto expectedResult = GetEmployeeProjectsPositionsDummyData().First().To<ProjectParticipantChangeDto>();
            expectedResult.ProjectPositionId = ProjectPosition_Id_2;
            expectedResult.EmployeeId = "InvalidId";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesProjectsPositionsService.ChangeEmployeeProjectPositionAsync(expectedResult));

            Assert.Equal(ErrorMessages.ProjectParticipantNullReference, ex.Message);
        }

        [Fact]
        public async Task ChangeEmployeeProjectPositionAsync_WithInvalidProjectPositionId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            ProjectParticipantChangeDto expectedResult = GetEmployeeProjectsPositionsDummyData().First().To<ProjectParticipantChangeDto>();
            int invalidProjectPositionId = 22;
            expectedResult.ProjectPositionId = invalidProjectPositionId;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesProjectsPositionsService.ChangeEmployeeProjectPositionAsync(expectedResult));

            Assert.Equal(string.Format(ErrorMessages.ProjectPositionNullReference, invalidProjectPositionId), ex.Message);
        }

        [Fact]
        public async Task RemoveParticipantAsync_WithValidData_ShouldRemoveProjectParticipantAndReturnTrue()
        {
            string errorMessagePrefix = "EmployeesProjectsPositions RemoveParticipantAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            ProjectParticipantChangeDto entityToRemove = (await context.EmployeesProjectsRoles.FirstAsync()).To<ProjectParticipantChangeDto>();
            var expectedCount = context.EmployeesProjectsRoles.Count() - 1;

            bool result = await this.employeesProjectsPositionsService.RemoveParticipantAsync(entityToRemove);

            var actualCount = context.EmployeesProjectsRoles.Count();
            
            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedCount == actualCount, errorMessagePrefix);
        }

        [Fact]
        public async Task RemoveParticipantAsync_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            ProjectParticipantChangeDto entityToRemove = (await context.EmployeesProjectsRoles.FirstAsync()).To<ProjectParticipantChangeDto>();
            entityToRemove.EmployeeId = "Invalid id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesProjectsPositionsService.RemoveParticipantAsync(entityToRemove));

            Assert.Equal(ErrorMessages.ProjectParticipantNullReference, ex.Message);
        }

        [Fact]
        public async Task RemoveParticipantAsync_WithInvalidProjectId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            ProjectParticipantChangeDto entityToRemove = (await context.EmployeesProjectsRoles.FirstAsync()).To<ProjectParticipantChangeDto>();
            entityToRemove.ProjectId = "Invalid id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesProjectsPositionsService.RemoveParticipantAsync(entityToRemove));

            Assert.Equal(ErrorMessages.ProjectParticipantNullReference, ex.Message);
        }

        [Fact]
        public async Task RemoveParticipantsAsync_WithValidData_ShouldRemoveProjectParticipantsAndReturnTrue()
        {
            string errorMessagePrefix = "EmployeesProjectsPositions RemoveParticipantsAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            List<ProjectEditParticipantDto> entitiesToRemove = GetEmployeeProjectsPositionsDummyData().Where(epp => epp.ProjectId == Project_Id_1).To<ProjectEditParticipantDto>().ToList();
            var expectedCount = context.EmployeesProjectsRoles.Count() - entitiesToRemove.Count;

            bool result = await this.employeesProjectsPositionsService.RemoveParticipantsAsync(entitiesToRemove);

            var actualCount = context.EmployeesProjectsRoles.Count();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedCount == actualCount, errorMessagePrefix);
        }

        [Fact]
        public async Task RemoveParticipantsAsync_WithEmptyData_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            string invalidId = "Invalid id";

            List<ProjectEditParticipantDto> entitiesToRemove = GetEmployeeProjectsPositionsDummyData().Where(epp => epp.ProjectId == invalidId).To<ProjectEditParticipantDto>().ToList();

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesProjectsPositionsService.RemoveParticipantsAsync(entitiesToRemove));

            Assert.Equal(ErrorMessages.ProjectParticipantsToRemoveNullReference, ex.Message);
        }

        [Fact]
        public async Task AddParticipantsAsync_WithValidData_ShouldAddProjectParticipantsAndReturnTrue()
        {
            string errorMessagePrefix = "EmployeesProjectsPositions AddParticipantsAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.employeesProjectsPositionsService = new EmployeesProjectsPositionsService(context);

            List<EmployeesProjectsPositions> newEntities = new List<EmployeesProjectsPositions>
            {
                new EmployeesProjectsPositions { EmployeeId = Employee_Id_4, ProjectId = Project_Id_1, ProjectPositionId = ProjectPosition_Id_2},
                new EmployeesProjectsPositions { EmployeeId = Employee_Id_5, ProjectId = Project_Id_1, ProjectPositionId = ProjectPosition_Id_3},
            };

            List<ProjectEditParticipantDto> entitiesToAdd = newEntities.To<ProjectEditParticipantDto>().ToList();

            var expectedCount = context.EmployeesProjectsRoles.Count() + entitiesToAdd.Count;

            bool result = await this.employeesProjectsPositionsService.AddParticipantsAsync(entitiesToAdd);

            var actualCount = context.EmployeesProjectsRoles.Count();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedCount == actualCount, errorMessagePrefix);
        }
    }
}
