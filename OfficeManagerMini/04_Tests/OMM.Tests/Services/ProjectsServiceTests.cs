using Microsoft.EntityFrameworkCore;
using Moq;
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
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class ProjectsServiceTests
    {
        #region ProjectsDummyData
        private const string Project_Id_1 = "01";
        private const string Project_Id_2 = "02";
        private const string Project_Id_3 = "03";
        private const string Project_Id_4 = "04";
        private const string Project_Id_5 = "05";

        private const string Project_Name_1 = "Project name 1";
        private const string Project_Name_2 = "Project name 2";
        private const string Project_Name_3 = "Project name 3";
        private const string Project_Name_4 = "Project name 4";
        private const string Project_Name_5 = "Project name 5";

        private const string Project_Client_1 = "Client 1";
        private const string Project_Client_2 = "Client 2";
        private const string Project_Client_3 = "Client 3";
        private const string Project_Client_4 = "Client 4";
        private const string Project_Client_5 = "Client 5";

        private const string Project_Description_1 = "Random project description 1";
        private const string Project_Description_2 = "Random project description 2";
        private const string Project_Description_3 = "Random project description 3";
        private const string Project_Description_4 = "Random project description 4";
        private const string Project_Description_5 = "Random project description 5";
        #endregion

        #region Priority
        private const int Priority_3 = 3;
        private const int Priority_5 = 5;
        private const int Priority_7 = 7;
        private const int Priority_10 = 10;
        #endregion

        #region Progress
        private const double Progress_0 = 0.0;
        private const double Progress_255 = 25.5;
        private const double Progress_50 = 50.0;
        private const double Progress_755 = 75.5;
        private const double Progress_100 = 100.0;
        #endregion

        #region StatusesDummyData
        private const int Status_Id_InProgress = 1;
        private const int Status_Id_Frozen = 2;
        private const int Status_Id_Waiting = 3;
        private const int Status_Id_Delayed = 4;
        private const int Status_Id_Completed = 5;

        private const string Status_InProgress = "In Progress";
        private const string Status_Frozen = "Frozen";
        private const string Status_Waiting = "Waiting";
        private const string Status_Delayed = "Delayed";
        private const string Status_Completed = "Completed";
        #endregion

        #region DepartmentsDummyData
        private const int Department_Id_1 = 1;
        private const int Department_Id_2 = 2;
        private const int Department_Id_3 = 3;
        private const int Department_Id_4 = 4;
        private const int Department_Id_5 = 5;

        private const string Department_Name_1 = "Management board";
        private const string Department_Name_2 = "HR";
        private const string Department_Name_3 = "Accounting";
        private const string Department_Name_4 = "Engineering";
        private const string Department_Name_5 = "Administration";
        #endregion

        #region ProjectPositionsDummyData
        private const string ProjectPosition_ProjectMananger_Name = "Project Manager";
        private const int ProjectPosition_ProjectMananger_Id = 1;

        private const string ProjectPosition_Participant_Name = "Participant";
        private const int ProjectPosition_Participant_Id = 2;

        private const string ProjectPosition_Assistant_Name = "Assistant";
        private const int ProjectPosition_Assistant_Id = 3;
        #endregion

        #region EmployeeDummyData
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

        private const string Employee_ProfilePicture_1 = "Link profile picture 1";
        private const string Employee_ProfilePicture_2 = "Link profile picture 2";
        private const string Employee_ProfilePicture_3 = "Link profile picture 3";
        private const string Employee_ProfilePicture_4 = "Link profile picture 4";
        private const string Employee_ProfilePicture_5 = "Link profile picture 5";
        #endregion

        #region AssignmentDummyData
        private const string Assignment_Id_1 = "1";
        private const string Assignment_Id_2 = "2";
        private const string Assignment_Id_3 = "3";

        private const string Assignment_Name_1 = "Assignment Name 1";
        private const string Assignment_Name_2 = "Assignment Name 2";
        private const string Assignment_Name_3 = "Assignment Name 3";

        private const string Assignment_Description_1 = "Assignment random text description 1";
        private const string Assignment_Description_2 = "Assignment random text description 2";
        private const string Assignment_Description_3 = "Assignment random text description 3";

        private const string Assignment_Type_1 = "Offer";
        private const string Assignment_Type_2 = "Report";
        private const string Assignment_Type_3 = "Invoice";

        private const bool Assignment_IsProjectRelated_YES = true;
        #endregion

        #region ReportData
        private const string Report_Id_1 = "0001";
        private const string Report_Id_2 = "0002";
        private const string Report_Id_3 = "0003";
        private const string Report_Id_4 = "0004";
        private const string Report_Id_5 = "0005";
        #endregion

        private const string Invalid_String_Id = "Invalid id";
        private const int Invalid_Int_Id = 18;

        private IProjectsService projectsService;

        private List<Department> GetDepartmentsDummyData()
        {
            return new List<Department>
            {
                new Department { Id = Department_Id_1, Name = Department_Name_1},
                new Department { Id = Department_Id_2, Name = Department_Name_2},
                new Department { Id = Department_Id_3, Name = Department_Name_3},
                new Department { Id = Department_Id_4, Name = Department_Name_4},
                new Department { Id = Department_Id_5, Name = Department_Name_5},
            };
        }

        private List<Status> GetStatusesDummyData()
        {
            return new List<Status>
            {
                new Status { Id = Status_Id_InProgress, Name = Status_InProgress},
                new Status { Id = Status_Id_Frozen, Name = Status_Frozen},
                new Status { Id = Status_Id_Waiting, Name = Status_Waiting},
                new Status { Id = Status_Id_Delayed, Name = Status_Delayed},
                new Status { Id = Status_Id_Completed, Name = Status_Completed},
            };
        }

        private List<ProjectPosition> GetProjectPositionsDummyData()
        {
            return new List<ProjectPosition>
            {
                new ProjectPosition { Id = ProjectPosition_ProjectMananger_Id, Name = ProjectPosition_ProjectMananger_Name},
                new ProjectPosition { Id = ProjectPosition_Participant_Id, Name = ProjectPosition_Participant_Name},
                new ProjectPosition { Id = ProjectPosition_Assistant_Id, Name = ProjectPosition_Assistant_Name},
            };
        }

        private List<Employee> GetEmployeesDummyData()
        {
            return new List<Employee>
            {
                new Employee { Id = Employee_Id_1, FullName = Employee_FullName_1, ProfilePicture = Employee_ProfilePicture_1, DepartmentId = Department_Id_1},
                new Employee { Id = Employee_Id_2, FullName = Employee_FullName_2, ProfilePicture = Employee_ProfilePicture_2, DepartmentId = Department_Id_2},
                new Employee { Id = Employee_Id_3, FullName = Employee_FullName_3, ProfilePicture = Employee_ProfilePicture_3, DepartmentId = Department_Id_2},
                new Employee { Id = Employee_Id_4, FullName = Employee_FullName_4, ProfilePicture = Employee_ProfilePicture_4, DepartmentId = Department_Id_3},
                new Employee { Id = Employee_Id_5, FullName = Employee_FullName_5, ProfilePicture = Employee_ProfilePicture_5, DepartmentId = Department_Id_4},
            };
        }

        private List<Project> GetProjectsDummyData()
        {
            return new List<Project>
            {
                new Project
                {
                    Id = Project_Id_1,
                    Name = Project_Name_1,
                    Client = Project_Client_1,
                    Description = Project_Description_1,
                    StatusId = Status_Id_InProgress,
                    CreatedOn = DateTime.UtcNow.AddDays(-20),
                    Progress = Progress_0,
                    Priority = Priority_5,
                    Report = new Report { Id = Report_Id_1},
                    Participants = new List<EmployeesProjectsPositions>
                    {
                        new EmployeesProjectsPositions{ EmployeeId = Employee_Id_1, ProjectPositionId = ProjectPosition_ProjectMananger_Id},
                        new EmployeesProjectsPositions{ EmployeeId = Employee_Id_2, ProjectPositionId = ProjectPosition_Participant_Id},
                    },
                },
                new Project
                {
                    Id = Project_Id_2,
                    Name = Project_Name_2,
                    Client = Project_Client_2,
                    Description = Project_Description_2,
                    StatusId = Status_Id_Completed,
                    CreatedOn = DateTime.UtcNow.AddDays(-25),
                    EndDate = DateTime.UtcNow.AddDays(-5),
                    Progress = Progress_100,
                    Priority = Priority_7,
                    Report = new Report { Id = Report_Id_2},
                    Participants = new List<EmployeesProjectsPositions>
                    {
                        new EmployeesProjectsPositions{ EmployeeId = Employee_Id_1, ProjectPositionId = ProjectPosition_ProjectMananger_Id}
                    },
                },
                new Project
                {
                    Id = Project_Id_3,
                    Name = Project_Name_3,
                    Client = Project_Client_3,
                    Description = Project_Description_3,
                    StatusId = Status_Id_InProgress,
                    CreatedOn = DateTime.UtcNow.AddDays(-15),
                    Deadline = DateTime.UtcNow.AddDays(-2),
                    Progress = Progress_255,
                    Priority = Priority_10,
                    Report = new Report { Id = Report_Id_3},
                    Participants = new List<EmployeesProjectsPositions>
                    {
                        new EmployeesProjectsPositions{ EmployeeId = Employee_Id_1, ProjectPositionId = ProjectPosition_ProjectMananger_Id},
                        new EmployeesProjectsPositions{ EmployeeId = Employee_Id_2, ProjectPositionId = ProjectPosition_ProjectMananger_Id},
                        new EmployeesProjectsPositions{ EmployeeId = Employee_Id_3, ProjectPositionId = ProjectPosition_Participant_Id},
                        new EmployeesProjectsPositions{ EmployeeId = Employee_Id_4, ProjectPositionId = ProjectPosition_Assistant_Id},
                    },
                },
            };
        }

        private List<Assignment> GetAssignmentsDummyData()
        {
            return new List<Assignment>
            {
                new Assignment
                {
                    Id = Assignment_Id_1,
                    Name = Assignment_Name_1,
                    Description = Assignment_Description_1,
                    Type = Assignment_Type_1,
                    IsProjectRelated = Assignment_IsProjectRelated_YES,
                    ProjectId = Project_Id_3,
                    Progress = Progress_0,
                    StartingDate = DateTime.UtcNow.AddDays(-15),
                    StatusId = Status_Id_InProgress,
                    Priority = Priority_3,
                    AssignorId = Employee_Id_1,
                    ExecutorId = Employee_Id_2,
                },
                new Assignment
                {
                    Id = Assignment_Id_2,
                    Name = Assignment_Name_2,
                    Description = Assignment_Description_2,
                    Type = Assignment_Type_2,
                    IsProjectRelated = Assignment_IsProjectRelated_YES,
                    ProjectId = Project_Id_3,
                    Progress = Progress_255,
                    StartingDate = DateTime.UtcNow.AddDays(-10),
                    StatusId = Status_Id_Frozen,
                    Priority = Priority_5,
                    AssignorId = Employee_Id_2,
                    ExecutorId = Employee_Id_3,
                },
                new Assignment
                {
                    Id = Assignment_Id_3,
                    Name = Assignment_Name_3,
                    Description = Assignment_Description_3,
                    Type = Assignment_Type_3,
                    IsProjectRelated = Assignment_IsProjectRelated_YES,
                    ProjectId = Project_Id_2,
                    Progress = Progress_100,
                    StartingDate = DateTime.UtcNow.AddDays(-25),
                    StatusId = Status_Id_Completed,
                    EndDate = DateTime.UtcNow.AddDays(-5),
                    Deadline = DateTime.UtcNow.AddDays(-4),
                    Priority = Priority_10,
                    AssignorId = Employee_Id_3,
                    ExecutorId = Employee_Id_2,
                },
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetStatusesDummyData());
            await context.AddRangeAsync(GetDepartmentsDummyData());
            await context.AddRangeAsync(GetProjectPositionsDummyData());
            await context.SaveChangesAsync();

            await context.AddRangeAsync(GetEmployeesDummyData());
            await context.SaveChangesAsync();

            await context.AddRangeAsync(GetProjectsDummyData());
            await context.SaveChangesAsync();

            await context.AddRangeAsync(GetAssignmentsDummyData());
            await context.SaveChangesAsync();
        }

        public ProjectsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetAllProjectsForList_WithDummyData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "ProjectsService GetAllProjectsForList() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            List<ProjectListDto> expectedResults = context.Projects.To<ProjectListDto>().ToList();
            List<ProjectListDto> actualResults = this.projectsService.GetAllProjectsForList().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            }

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returend projects is not correct!");
        }

        [Fact]
        public async Task GetAllProjectsForList_WithZeroData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "ProjectsService GetAllProjectsForList() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            List<ProjectListDto> actualResults = await this.projectsService.GetAllProjectsForList().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returend projects is not correct!");
        }

        [Fact]
        public async Task CreateProjectAsync_WithValidDataAndStatusNotCompleted_ShouldCreateProjectAndCallCreateReportAndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService CreateProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            reportsService.Setup(rs => rs.CreateReportAsync(It.IsAny<string>())).ReturnsAsync(true);

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) =>
                         {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var projectToCreate = new ProjectCreateDto
            {
                Name = Project_Name_5,
                Client = Project_Client_5,
                Description = Project_Description_5,
                Priority = Priority_3,
                Progress = Progress_0,
                CreatedOn = "30-08-2019",
                StatusId = Status_Id_InProgress,
                Participants = new List<ProjectParticipantListDto>
                {
                    new ProjectParticipantListDto { EmployeeId = Employee_Id_3, ProjectPositionId = ProjectPosition_ProjectMananger_Id},
                    new ProjectParticipantListDto { EmployeeId = Employee_Id_4, ProjectPositionId = ProjectPosition_Assistant_Id},
                },
            };
            var expectedCount = context.Projects.Count() + 1;

            bool actualResult = await this.projectsService.CreateProjectAsync(projectToCreate);
            var actualCount = context.Projects.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.True(expectedCount == actualCount, errorMessagePrefix);
            reportsService.Verify(rs => rs.CreateReportAsync(It.IsAny<string>()), Times.Once, errorMessagePrefix + " " + "Creating report is not called!");
        }
        [Fact]
        public async Task CreateProjectAsync_WithValidDataAndStatusCompleted_ShouldCreateProjectAndSetEndDataToDateAndProgressTo100AndCallCreateReportAndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService CreateProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            reportsService.Setup(rs => rs.CreateReportAsync(It.IsAny<string>())).ReturnsAsync(true);

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) =>
                         {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var projectToCreate = new ProjectCreateDto
            {
                Name = Project_Name_5,
                Client = Project_Client_5,
                Description = Project_Description_5,
                Priority = Priority_3,
                Progress = Progress_0,
                CreatedOn = "30-08-2019",
                StatusId = Status_Id_Completed,
                Participants = new List<ProjectParticipantListDto>
                {
                    new ProjectParticipantListDto { EmployeeId = Employee_Id_3, ProjectPositionId = ProjectPosition_ProjectMananger_Id},
                    new ProjectParticipantListDto { EmployeeId = Employee_Id_4, ProjectPositionId = ProjectPosition_Assistant_Id},
                },
            };
            var expectedCount = context.Projects.Count() + 1;

            bool result = await this.projectsService.CreateProjectAsync(projectToCreate);
            var actualResult = await context.Projects.SingleAsync(p => p.Name == Project_Name_5);
            var actualCount = context.Projects.Count();

            Assert.True(result, errorMessagePrefix);
            Assert.True(actualResult.StatusId == Status_Id_Completed, errorMessagePrefix + " " + "StatusId is not set corretly!");
            Assert.True(actualResult.EndDate.HasValue, errorMessagePrefix + " " + "EndDate is not set corretly!");
            Assert.True(actualResult.Progress == Progress_100, errorMessagePrefix + " " + "Progress is not set corretly!");
            Assert.True(expectedCount == actualCount, errorMessagePrefix);
            reportsService.Verify(rs => rs.CreateReportAsync(It.IsAny<string>()), Times.Once, errorMessagePrefix + " " + "Creating report is not called!");
        }

        [Fact]
        public async Task GetAllProjects_WithDummyData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "ProjectsService GetAllProjects() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            List<ProjectAllListDto> expectedResults = context.Projects.To<ProjectAllListDto>().ToList();
            List<ProjectAllListDto> actualResults = this.projectsService.GetAllProjects().ToList();

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returend projects is not correct!");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
                Assert.True(expectedEntry.Client == actualEntry.Client, errorMessagePrefix + " " + "Client is not returned properly.");
                Assert.True(expectedEntry.Priority == actualEntry.Priority, errorMessagePrefix + " " + "Priority is not returned properly.");
                Assert.True(expectedEntry.StatusName == actualEntry.StatusName, errorMessagePrefix + " " + "StatusName is not returned properly.");
                Assert.True(expectedEntry.Progress == actualEntry.Progress, errorMessagePrefix + " " + "Progress is not returned properly.");
                Assert.True(expectedEntry.CreatedOn == actualEntry.CreatedOn, errorMessagePrefix + " " + "CreatedOn is not returned properly.");
                Assert.True(expectedEntry.Deadline == actualEntry.Deadline, errorMessagePrefix + " " + "Deadline is not returned properly.");
                Assert.True(expectedEntry.EndDate == actualEntry.EndDate, errorMessagePrefix + " " + "EndDate is not returned properly.");
                Assert.True(expectedEntry.ParticipantsCount == actualEntry.ParticipantsCount, errorMessagePrefix + " " + "ParticipantsCount is not returned properly.");
                Assert.True(expectedEntry.AssignmentsCount == actualEntry.AssignmentsCount, errorMessagePrefix + " " + "AssignmentsCount is not returned properly.");
            }
        }

        [Fact]
        public async Task GetAllProjects_WithZeroData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "ProjectsService GetAllProjects() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            List<ProjectAllListDto> actualResults = await this.projectsService.GetAllProjects().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returend projects is not correct!");
        }

        [Fact]
        public async Task GetMyProjects_WithValidDataAndProjectsToEmployee_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "ProjectsService GetMyProjects() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            List<ProjectAllListDto> expectedResults = context.Projects.Where(p => p.Participants.Any(pp => pp.EmployeeId == Employee_Id_1)).To<ProjectAllListDto>().ToList();
            List<ProjectAllListDto> actualResults = this.projectsService.GetMyProjects(Employee_Id_1).ToList();

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returend projects is not correct!");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
                Assert.True(expectedEntry.Client == actualEntry.Client, errorMessagePrefix + " " + "Client is not returned properly.");
                Assert.True(expectedEntry.Priority == actualEntry.Priority, errorMessagePrefix + " " + "Priority is not returned properly.");
                Assert.True(expectedEntry.StatusName == actualEntry.StatusName, errorMessagePrefix + " " + "StatusName is not returned properly.");
                Assert.True(expectedEntry.Progress == actualEntry.Progress, errorMessagePrefix + " " + "Progress is not returned properly.");
                Assert.True(expectedEntry.CreatedOn == actualEntry.CreatedOn, errorMessagePrefix + " " + "CreatedOn is not returned properly.");
                Assert.True(expectedEntry.Deadline == actualEntry.Deadline, errorMessagePrefix + " " + "Deadline is not returned properly.");
                Assert.True(expectedEntry.EndDate == actualEntry.EndDate, errorMessagePrefix + " " + "EndDate is not returned properly.");
                Assert.True(expectedEntry.ParticipantsCount == actualEntry.ParticipantsCount, errorMessagePrefix + " " + "ParticipantsCount is not returned properly.");
                Assert.True(expectedEntry.AssignmentsCount == actualEntry.AssignmentsCount, errorMessagePrefix + " " + "AssignmentsCount is not returned properly.");
            }
        }

        [Fact]
        public async Task GetMyProjects_WithValidDataAndZeroProjectsToEmployee_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "ProjectsService GetMyProjects() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            List<ProjectAllListDto> actualResults = this.projectsService.GetMyProjects(Employee_Id_5).ToList();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returend projects is not correct!");
        }

        [Fact]
        public async Task GetMyProjects_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var ex = Assert.Throws<NullReferenceException>(() => this.projectsService.GetMyProjects(Invalid_String_Id));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, Invalid_String_Id), ex.Message);
        }

        [Theory]
        [InlineData(Project_Id_1)]
        [InlineData(Project_Id_2)]
        [InlineData(Project_Id_3)]
        public async Task GetProjectById_WithValidData_ShouldReturnCorrectResult(string projectId)
        {
            string errorMessagePrefix = "ProjectsService GetProjectById() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            List<ProjectEditDto> expectedResults = context.Projects.Where(p => p.Id == projectId).To<ProjectEditDto>().ToList();
            List<ProjectEditDto> actualResults = this.projectsService.GetProjectById<ProjectEditDto>(projectId).ToList();

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returend projects is not correct!");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
                Assert.True(expectedEntry.Client == actualEntry.Client, errorMessagePrefix + " " + "Client is not returned properly.");
                Assert.True(expectedEntry.Priority == actualEntry.Priority, errorMessagePrefix + " " + "Priority is not returned properly.");
                Assert.True(expectedEntry.StatusId == actualEntry.StatusId, errorMessagePrefix + " " + "StatusId is not returned properly.");
                Assert.True(expectedEntry.Progress == actualEntry.Progress, errorMessagePrefix + " " + "Progress is not returned properly.");
                Assert.True(expectedEntry.CreatedOn == actualEntry.CreatedOn, errorMessagePrefix + " " + "CreatedOn is not returned properly.");
                Assert.True(expectedEntry.Deadline == actualEntry.Deadline, errorMessagePrefix + " " + "Deadline is not returned properly.");
                Assert.True(expectedEntry.Participants.Count == actualEntry.Participants.Count, errorMessagePrefix + " " + "Participants are not returned properly.");
            }
        }

        [Fact]
        public async Task GetProjectById_WithInvalidProjectId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var ex = Assert.Throws<NullReferenceException>(() => this.projectsService.GetProjectById<ProjectEditDto>(Invalid_String_Id));

            Assert.Equal(string.Format(ErrorMessages.ProjectIdNullReference, Invalid_String_Id), ex.Message);
        }

        [Fact]
        public async Task ChangeDataAsync_SetStatusFromNotCompletedToNotCompletedAndChangeProgress_ShouldChangeStatusAndProgressCorrectlyAndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) =>
                         {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) =>
                         {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectDetailsChangeDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectDetailsChangeDto>();

            expectedResult.StatusId = Status_Id_Frozen;
            expectedResult.StatusName = Status_Frozen;
            expectedResult.Progress = Progress_50;

            bool result = await this.projectsService.ChangeDataAsync(expectedResult);

            ProjectDetailsChangeDto actualResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectDetailsChangeDto>();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedResult.StatusId == actualResult.StatusId, errorMessagePrefix + " " + "StatusId is not changed correctly!");
            Assert.True(expectedResult.StatusName == actualResult.StatusName, errorMessagePrefix + " " + "StatusName is not changed correctly!");
            Assert.True(expectedResult.Progress == actualResult.Progress, errorMessagePrefix + " " + "Progress is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_SetOnlyStatusToCompletedWithNoEndDate_ShouldSetEndDateToDateAndProgressTo100AndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) =>
                         {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) =>
                         {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectDetailsChangeDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectDetailsChangeDto>();

            expectedResult.StatusId = Status_Id_Completed;

            bool result = await this.projectsService.ChangeDataAsync(expectedResult);

            Project actualResult = await context.Projects.SingleAsync(p => p.Id == Project_Id_1);

            Assert.True(result, errorMessagePrefix);
            Assert.True(actualResult.StatusId == Status_Id_Completed, errorMessagePrefix + " " + "StatusId is not changed correctly!");
            Assert.True(actualResult.Progress == Progress_100, errorMessagePrefix + " " + "Progress is not changed correctly!");
            Assert.True(actualResult.EndDate.HasValue, errorMessagePrefix + " " + "EndDate is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_SetOnlyEndDateFromDateToNullWhenStatusIsCompleted_ShouldChangeStatusToInProgressAndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) =>
                         {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) =>
                         {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectDetailsChangeDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_2)).To<ProjectDetailsChangeDto>();

            expectedResult.EndDate = null;

            bool result = await this.projectsService.ChangeDataAsync(expectedResult);

            Project actualResult = await context.Projects.SingleAsync(p => p.Id == Project_Id_2);

            Assert.True(result, errorMessagePrefix);
            Assert.True(actualResult.StatusId == Status_Id_InProgress, errorMessagePrefix + " " + "StatusId is not changed correctly!");
            Assert.True(expectedResult.Progress == actualResult.Progress, errorMessagePrefix + " " + "Progress is not changed correctly!");
            Assert.False(actualResult.EndDate.HasValue, errorMessagePrefix + " " + "EndDate is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_SetOnlyEndDateFromNullToDateWhenStatusIsNotCompleted_ShouldChangeStatusToCompletedAndProgressTo100AndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) =>
                         {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) =>
                         {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectDetailsChangeDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectDetailsChangeDto>();

            expectedResult.EndDate = "29-08-2019";

            bool result = await this.projectsService.ChangeDataAsync(expectedResult);

            ProjectDetailsChangeDto actualResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectDetailsChangeDto>();

            Assert.True(result, errorMessagePrefix);
            Assert.True(actualResult.StatusId == Status_Id_Completed, errorMessagePrefix + " " + "StatusId is not changed correctly!");
            Assert.True(actualResult.StatusName == Status_Completed, errorMessagePrefix + " " + "StatusName is not changed correctly!");
            Assert.True(actualResult.Progress == Progress_100, errorMessagePrefix + " " + "Progress is not changed correctly!");
            Assert.True(expectedResult.EndDate == actualResult.EndDate, errorMessagePrefix + " " + "EndDate is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_ChangeDeadlineFromDateToNull_ShouldChangeProjectCorrectlyAndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) =>
                         {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) =>
                         {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectDetailsChangeDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_3)).To<ProjectDetailsChangeDto>();

            expectedResult.Deadline = null;

            bool result = await this.projectsService.ChangeDataAsync(expectedResult);

            Project actualResult = await context.Projects.SingleAsync(p => p.Id == Project_Id_3);

            Assert.True(result, errorMessagePrefix);
            Assert.False(actualResult.Deadline.HasValue, errorMessagePrefix + " " + "Deadline is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_ChangeDeadlineFromNullToDate_ShouldChangeProjectCorrectlyAndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) =>
                         {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) =>
                         {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectDetailsChangeDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectDetailsChangeDto>();

            expectedResult.Deadline = "29-08-2019";

            bool result = await this.projectsService.ChangeDataAsync(expectedResult);

            ProjectDetailsChangeDto actualResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectDetailsChangeDto>();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedResult.Deadline == actualResult.Deadline, errorMessagePrefix + " " + "Deadline is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_WithInvalidAssignmnentId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var input = (await context.Projects.FirstAsync()).To<ProjectDetailsChangeDto>();

            input.Id = Invalid_String_Id;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.projectsService.ChangeDataAsync(input));
            Assert.Equal(string.Format(ErrorMessages.ProjectIdNullReference, Invalid_String_Id), ex.Message);
        }

        [Fact]
        public async Task AddParticipantAsync_WithValidData_ShouldAddParticipantAndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService AddParticipantAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var expectedParticipantsCount = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).Participants.Count() + 1;
            ProjectParticipantDto expectedResult = new ProjectParticipantDto { EmployeeId = Employee_Id_5, ProjectId = Project_Id_1, ProjectPositionId = ProjectPosition_Assistant_Id };

            var result = await this.projectsService.AddParticipantAsync(expectedResult);

            var actualResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).Participants.ToList();
            var actualParticipantsCount = actualResult.Count();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedParticipantsCount == actualParticipantsCount, errorMessagePrefix + " " + "The participant was not added!");
            Assert.Contains(Employee_Id_5, actualResult.Select(p => p.EmployeeId));
        }

        [Fact]
        public async Task CheckParticipantAsync_WithValidParticipant_ShouldReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService CheckParticipantAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectParticipantDto expectedResult = new ProjectParticipantDto { EmployeeId = Employee_Id_1, ProjectId = Project_Id_1, ProjectPositionId = ProjectPosition_ProjectMananger_Id };

            var result = await this.projectsService.CheckParticipantAsync(expectedResult);

            Assert.True(result, errorMessagePrefix);
        }

        [Fact]
        public async Task CheckParticipantAsync_WithInvalidParticipant_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProjectsService CheckParticipantAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectParticipantDto expectedResult = new ProjectParticipantDto { EmployeeId = Employee_Id_5, ProjectId = Project_Id_1, ProjectPositionId = ProjectPosition_ProjectMananger_Id };

            var result = await this.projectsService.CheckParticipantAsync(expectedResult);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task CheckIsParticipantLastManagerAsync_WhenParticipantIsNotProjectManager_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProjectsService CheckIsParticipantLastManagerAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();

            var projectPositionsService = new Mock<IProjectPositionsService>();
            projectPositionsService.Setup(pps => pps.GetProjectPositionNameByIdAsync(It.IsAny<int>()))
                                   .ReturnsAsync((int id) =>
                                                {
                                                    if (id == ProjectPosition_ProjectMananger_Id) return ProjectPosition_ProjectMananger_Name;
                                                    else if (id == ProjectPosition_Participant_Id) return ProjectPosition_Participant_Name;
                                                    else return ProjectPosition_Assistant_Name;
                                                });

            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectParticipantChangeDto projectParticipantToChangePosition = new ProjectParticipantChangeDto
            {
                EmployeeId = Employee_Id_3,
                ProjectId = Project_Id_3,
                ProjectPositionId = ProjectPosition_Assistant_Id
            };

            var result = await this.projectsService.CheckIsParticipantLastManagerAsync(projectParticipantToChangePosition);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task CheckIsParticipantLastManagerAsync_WhenParticipantIsProjectManagerButNotLastAndChangeToOtherPosition_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProjectsService CheckIsParticipantLastManagerAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();

            var projectPositionsService = new Mock<IProjectPositionsService>();
            projectPositionsService.Setup(pps => pps.GetProjectPositionNameByIdAsync(It.IsAny<int>()))
                                   .ReturnsAsync((int id) =>
                                   {
                                       if (id == ProjectPosition_ProjectMananger_Id) return ProjectPosition_ProjectMananger_Name;
                                       else if (id == ProjectPosition_Participant_Id) return ProjectPosition_Participant_Name;
                                       else return ProjectPosition_Assistant_Name;
                                   });

            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectParticipantChangeDto projectParticipantToChangePosition = new ProjectParticipantChangeDto
            {
                EmployeeId = Employee_Id_2,
                ProjectId = Project_Id_3,
                ProjectPositionId = ProjectPosition_Assistant_Id
            };

            var result = await this.projectsService.CheckIsParticipantLastManagerAsync(projectParticipantToChangePosition);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task CheckIsParticipantLastManagerAsync_WhenParticipantIsProjectManagerAndIsLast_ShouldReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService CheckIsParticipantLastManagerAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();

            var projectPositionsService = new Mock<IProjectPositionsService>();
            projectPositionsService.Setup(pps => pps.GetProjectPositionNameByIdAsync(It.IsAny<int>()))
                                   .ReturnsAsync((int id) =>
                                   {
                                       if (id == ProjectPosition_ProjectMananger_Id) return ProjectPosition_ProjectMananger_Name;
                                       else if (id == ProjectPosition_Participant_Id) return ProjectPosition_Participant_Name;
                                       else return ProjectPosition_Assistant_Name;
                                   });

            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectParticipantChangeDto projectParticipantToChangePosition = new ProjectParticipantChangeDto
            {
                EmployeeId = Employee_Id_1,
                ProjectId = Project_Id_1,
                ProjectPositionId = ProjectPosition_Assistant_Id
            };

            var result = await this.projectsService.CheckIsParticipantLastManagerAsync(projectParticipantToChangePosition);

            Assert.True(result, errorMessagePrefix);
        }

        [Fact]
        public async Task CheckIsParticipantLastManagerAsync_WithInvalidProjectId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();

            var projectPositionsService = new Mock<IProjectPositionsService>();
            projectPositionsService.Setup(pps => pps.GetProjectPositionNameByIdAsync(It.IsAny<int>()))
                                   .ReturnsAsync((int id) =>
                                   {
                                       if (id == ProjectPosition_ProjectMananger_Id) return ProjectPosition_ProjectMananger_Name;
                                       else if (id == ProjectPosition_Participant_Id) return ProjectPosition_Participant_Name;
                                       else return ProjectPosition_Assistant_Name;
                                   });

            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectParticipantChangeDto projectParticipantToChangePosition = new ProjectParticipantChangeDto
            {
                EmployeeId = Employee_Id_1,
                ProjectId = Project_Id_5,
                ProjectPositionId = ProjectPosition_Assistant_Id
            };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.projectsService.CheckIsParticipantLastManagerAsync(projectParticipantToChangePosition));

            Assert.Equal(string.Format(ErrorMessages.ProjectIdNullReference, Project_Id_5), ex.Message);
        }

        [Fact]
        public async Task CheckIsParticipantLastManagerAsync_WithInvalidParticipantId_ShouldThrowArgumentException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();

            var projectPositionsService = new Mock<IProjectPositionsService>();
            projectPositionsService.Setup(pps => pps.GetProjectPositionNameByIdAsync(It.IsAny<int>()))
                                   .ReturnsAsync((int id) =>
                                   {
                                       if (id == ProjectPosition_ProjectMananger_Id) return ProjectPosition_ProjectMananger_Name;
                                       else if (id == ProjectPosition_Participant_Id) return ProjectPosition_Participant_Name;
                                       else return ProjectPosition_Assistant_Name;
                                   });

            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectParticipantChangeDto projectParticipantToChangePosition = new ProjectParticipantChangeDto
            {
                EmployeeId = Employee_Id_5,
                ProjectId = Project_Id_1,
                ProjectPositionId = ProjectPosition_Assistant_Id
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => this.projectsService.CheckIsParticipantLastManagerAsync(projectParticipantToChangePosition));

            Assert.Equal(string.Format(ErrorMessages.ProjectParticipantArgumentException, Employee_Id_5, Project_Id_1), ex.Message);
        }

        [Fact]
        public async Task IsEmployeeAuthorizedToChangeProject_WithEmployeeInProjectManagerPosition_ShouldReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService IsEmployeeAuthorizedToChangeProject() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            employeesService.Setup(es => es.CheckIfEmployeeIsInRole(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var result = await this.projectsService.IsEmployeeAuthorizedToChangeProject(Project_Id_1, Employee_Id_1);

            Assert.True(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmployeeAuthorizedToChangeProject_WithEmployeeNotInProjectManagerPosition_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProjectsService IsEmployeeAuthorizedToChangeProject() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            employeesService.Setup(es => es.CheckIfEmployeeIsInRole(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var result = await this.projectsService.IsEmployeeAuthorizedToChangeProject(Project_Id_1, Employee_Id_2);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmployeeAuthorizedToChangeProject_WithEmployeeNotParticipantInProject_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProjectsService IsEmployeeAuthorizedToChangeProject() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            employeesService.Setup(es => es.CheckIfEmployeeIsInRole(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var result = await this.projectsService.IsEmployeeAuthorizedToChangeProject(Project_Id_1, Employee_Id_5);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmployeeAuthorizedToChangeProject_WithInvalidProjectId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            employeesService.Setup(es => es.CheckIfEmployeeIsInRole(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.projectsService.IsEmployeeAuthorizedToChangeProject(Invalid_String_Id, Employee_Id_1));

            Assert.Equal(string.Format(ErrorMessages.ProjectIdNullReference, Invalid_String_Id), ex.Message);
        }

        [Fact]
        public async Task IsEmployeeAuthorizedForProject_WithInvalidProjectId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            employeesService.Setup(es => es.CheckIfEmployeeIsInRole(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.projectsService.IsEmployeeAuthorizedForProject(Invalid_String_Id, Employee_Id_1));

            Assert.Equal(string.Format(ErrorMessages.ProjectIdNullReference, Invalid_String_Id), ex.Message);
        }

        [Fact]
        public async Task IsEmployeeAuthorizedForProject_WithEmployeeNotParticipantInProject_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProjectsService IsEmployeeAuthorizedForProject() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            employeesService.Setup(es => es.CheckIfEmployeeIsInRole(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var result = await this.projectsService.IsEmployeeAuthorizedForProject(Project_Id_1, Employee_Id_5);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmployeeAuthorizedForProject_WithEmployeeParticipantInProject_ShouldReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService IsEmployeeAuthorizedForProject() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            employeesService.Setup(es => es.CheckIfEmployeeIsInRole(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var result = await this.projectsService.IsEmployeeAuthorizedForProject(Project_Id_1, Employee_Id_2);

            Assert.True(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmployeeParticipant_WithEmployeeParticipantInProject_ShouldReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService IsEmployeeParticipant() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var result = await this.projectsService.IsEmployeeParticipant(Project_Id_1, Employee_Id_2);

            Assert.True(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmployeeParticipant_WithEmployeeNotParticipantInProject_ShouldReturnFalse()
        {
            string errorMessagePrefix = "ProjectsService IsEmployeeParticipant() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var result = await this.projectsService.IsEmployeeParticipant(Project_Id_1, Employee_Id_5);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmployeeParticipant_WithInvalidProjectId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.projectsService.IsEmployeeParticipant(Invalid_String_Id, Employee_Id_1));

            Assert.Equal(string.Format(ErrorMessages.ProjectIdNullReference, Invalid_String_Id), ex.Message);
        }

        [Fact]
        public async Task DeleteProjectAsync_WithInvalidProjectId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.projectsService.DeleteProjectAsync(Invalid_String_Id));

            Assert.Equal(string.Format(ErrorMessages.ProjectIdNullReference, Invalid_String_Id), ex.Message);
        }

        [Fact]
        public async Task DeleteProjectAsync_WithValidProjectIdAndNoAssignmentsToProject_ShouldDeleteProjectAndReturnTrue()
        {
            string errorMessagePrefix = "ProjectsService DeleteProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            var expectedProjectCount = context.Projects.Count() - 1;

            var result = await this.projectsService.DeleteProjectAsync(Project_Id_1);

            var actualProjectCount = context.Projects.Count();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedProjectCount == actualProjectCount, errorMessagePrefix);
            assignmentsService.Verify(ae => ae.DeleteProjectAssignmentsAsync(It.IsAny<List<Assignment>>()), Times.Never, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteProjectAsync_WithValidProjectIdAndAssignmentsToProject_ShouldCallAssignmentsServiceDeleteProjectAssignmentsAsync()
        {
            string errorMessagePrefix = "ProjectsService DeleteProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();
            var statusService = new Mock<IStatusesService>();
            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            await this.projectsService.DeleteProjectAsync(Project_Id_3);

            assignmentsService.Verify(ae => ae.DeleteProjectAssignmentsAsync(It.IsAny<List<Assignment>>()), Times.Once, errorMessagePrefix);
        }

        [Fact]
        public async Task EditProjectAsync_WithCommonDataWithoutIfStatements_ShouldEditProjectCorrectly()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectEditDto>();

            expectedResult.Name = Project_Name_5;
            expectedResult.Client = Project_Client_5;
            expectedResult.Description = Project_Description_5;
            expectedResult.Priority = Priority_10;
            expectedResult.CreatedOn = "28-07-2019";

            await this.projectsService.EditProjectAsync(expectedResult);

            ProjectEditDto actualResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectEditDto>();

            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not changed properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not changed properly.");
            Assert.True(expectedResult.Priority == actualResult.Priority, errorMessagePrefix + " " + "Priority is not changed properly.");
            Assert.True(expectedResult.Client == actualResult.Client, errorMessagePrefix + " " + "Client is not changed properly.");
            Assert.True(expectedResult.CreatedOn == actualResult.CreatedOn, errorMessagePrefix + " " + "CreatedOn is not changed properly.");
        }

        [Fact]
        public async Task EditProjectAsync_ChangeDeadlineFromNullToActualDate_ShouldEditProjectCorrectly()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectEditDto>();

            expectedResult.Deadline = "25-11-2019";

            await this.projectsService.EditProjectAsync(expectedResult);

            ProjectEditDto actualResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectEditDto>();

            Assert.True(expectedResult.Deadline == actualResult.Deadline, errorMessagePrefix + " " + "Deadline is not changed properly.");
        }

        [Fact]
        public async Task EditProjectAsync_ChangeDeadlineFromDateToNull_ShouldEditProjectCorrectly()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_3)).To<ProjectEditDto>();

            expectedResult.Deadline = null;

            await this.projectsService.EditProjectAsync(expectedResult);

            Project actualResult = await context.Projects.SingleAsync(p => p.Id == Project_Id_3);

            Assert.False(actualResult.Deadline.HasValue, errorMessagePrefix + " " + "Deadline is not changed properly.");
        }

        [Fact]
        public async Task EditProjectAsync_ChangeStatusFromNotCompletedToNotCompleted_ShouldEditProjectCorrectly()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectEditDto>();

            expectedResult.StatusId = Status_Id_Delayed;
            expectedResult.Progress = Progress_255;

            await this.projectsService.EditProjectAsync(expectedResult);

            Project actualResult = await context.Projects.SingleAsync(p => p.Id == Project_Id_1);

            Assert.True(expectedResult.StatusId == actualResult.StatusId, errorMessagePrefix + " " + "StatusId is not changed properly.");
            Assert.True(expectedResult.Progress == actualResult.Progress, errorMessagePrefix + " " + "Progress is not changed properly.");
            Assert.True(actualResult.EndDate == null, errorMessagePrefix + " " + "EndDate is not changed properly.");
        }

        [Fact]
        public async Task EditProjectAsync_ChangeStatusFromNotCompletedToCompleted_ShouldEditProjectCorrectly()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_Completed);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_1)).To<ProjectEditDto>();

            expectedResult.StatusId = Status_Id_Completed;

            await this.projectsService.EditProjectAsync(expectedResult);

            Project actualResult = await context.Projects.SingleAsync(p => p.Id == Project_Id_1);

            Assert.True(expectedResult.StatusId == actualResult.StatusId, errorMessagePrefix + " " + "StatusId is not changed properly.");
            Assert.True(actualResult.Progress == Progress_100, errorMessagePrefix + " " + "Progress is not changed properly.");
            Assert.True(actualResult.EndDate.HasValue, errorMessagePrefix + " " + "EndDate is not changed properly.");
        }

        [Fact]
        public async Task EditProjectAsync_ChangeStatusFromCompletedToNoTCompleted_ShouldEditProjectCorrectly()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_3)).To<ProjectEditDto>();

            expectedResult.StatusId = Status_Id_InProgress;
            expectedResult.Progress = Progress_755;

            await this.projectsService.EditProjectAsync(expectedResult);

            Project actualResult = await context.Projects.SingleAsync(p => p.Id == Project_Id_3);

            Assert.True(expectedResult.StatusId == actualResult.StatusId, errorMessagePrefix + " " + "StatusId is not changed properly.");
            Assert.True(expectedResult.Progress == actualResult.Progress, errorMessagePrefix + " " + "Progress is not changed properly.");
            Assert.False(actualResult.EndDate.HasValue, errorMessagePrefix + " " + "EndDate is not changed properly.");
        }

        [Fact]
        public async Task EditProjectAsync_DontChangeParticipants_ShouldNotCallEmployeesProjectsPositionsService()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_3)).To<ProjectEditDto>();

            expectedResult.Description = Project_Description_5;

            await this.projectsService.EditProjectAsync(expectedResult);

            Project actualResult = await context.Projects.SingleAsync(p => p.Id == Project_Id_3);

            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not changed properly.");
            employeesProjectsPositionsService.Verify(epps => epps.RemoveParticipantsAsync(It.IsAny<List<ProjectEditParticipantDto>>()), Times.Never, errorMessagePrefix);
            employeesProjectsPositionsService.Verify(epps => epps.AddParticipantsAsync(It.IsAny<List<ProjectEditParticipantDto>>()), Times.Never, errorMessagePrefix);
        }

        [Fact]
        public async Task EditProjectAsync_AddParticipants_ShouldCallAddParticipantsAsyncFromEmployeesProjectsPositionsService()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_2)).To<ProjectEditDto>();

            List<ProjectEditParticipantDto> participantsToAdd = new List<ProjectEditParticipantDto>
            {
                 new ProjectEditParticipantDto{ ProjectId = Project_Id_2, EmployeeId = Employee_Id_3, ProjectPositionId = ProjectPosition_Participant_Id},
                 new ProjectEditParticipantDto{ ProjectId = Project_Id_2, EmployeeId = Employee_Id_4, ProjectPositionId = ProjectPosition_Assistant_Id},
            };

            expectedResult.Participants.AddRange(participantsToAdd);

            await this.projectsService.EditProjectAsync(expectedResult);

            employeesProjectsPositionsService.Verify(epps => epps.RemoveParticipantsAsync(It.IsAny<List<ProjectEditParticipantDto>>()), Times.Never, errorMessagePrefix);
            employeesProjectsPositionsService.Verify(epps => epps.AddParticipantsAsync(It.IsAny<List<ProjectEditParticipantDto>>()), Times.Once, errorMessagePrefix);
        }

        [Fact]
        public async Task EditProjectAsync_RemoveParticipants_ShouldCallRemoveParticipantsAsyncFromEmployeesProjectsPositionsService()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_3)).To<ProjectEditDto>();

            List<string> participantsToRemove = new List<string>
            {
                 Employee_Id_2,
                 Employee_Id_3,
                 Employee_Id_4
            };

            for (int i = 0; i < participantsToRemove.Count; i++)
            {
                var participantToRemove = expectedResult.Participants.Single(p => p.EmployeeId == participantsToRemove[i]);
                expectedResult.Participants.Remove(participantToRemove);
            }

            await this.projectsService.EditProjectAsync(expectedResult);

            employeesProjectsPositionsService.Verify(epps => epps.RemoveParticipantsAsync(It.IsAny<List<ProjectEditParticipantDto>>()), Times.Once, errorMessagePrefix);
            employeesProjectsPositionsService.Verify(epps => epps.AddParticipantsAsync(It.IsAny<List<ProjectEditParticipantDto>>()), Times.Never, errorMessagePrefix);
        }

        [Fact]
        public async Task EditProjectAsync_AddAndRemoveParticipants_ShouldCallEmployeesProjectsPositionsService()
        {
            string errorMessagePrefix = "ProjectsService EditProjectAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var reportsService = new Mock<IReportsService>();

            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var employeesService = new Mock<IEmployeesService>();
            var projectPositionsService = new Mock<IProjectPositionsService>();
            var employeesProjectsPositionsService = new Mock<IEmployeesProjectsPositionsService>();
            var assignmentsService = new Mock<IAssignmentsService>();
            this.projectsService = new ProjectsService(
                                                        context,
                                                        reportsService.Object,
                                                        statusService.Object,
                                                        employeesService.Object,
                                                        projectPositionsService.Object,
                                                        employeesProjectsPositionsService.Object,
                                                        assignmentsService.Object
                                                       );

            ProjectEditDto expectedResult = (await context.Projects.SingleAsync(p => p.Id == Project_Id_3)).To<ProjectEditDto>();

            List<ProjectEditParticipantDto> participantsToAdd = new List<ProjectEditParticipantDto>
            {
                 new ProjectEditParticipantDto{ ProjectId = Project_Id_3, EmployeeId = Employee_Id_5, ProjectPositionId = ProjectPosition_Participant_Id},
            };

            expectedResult.Participants.AddRange(participantsToAdd);

            List<string> participantsToRemove = new List<string>
            {
                 Employee_Id_2,
                 Employee_Id_3,
            };

            for (int i = 0; i < participantsToRemove.Count; i++)
            {
                var participantToRemove = expectedResult.Participants.Single(p => p.EmployeeId == participantsToRemove[i]);
                expectedResult.Participants.Remove(participantToRemove);
            }

            await this.projectsService.EditProjectAsync(expectedResult);

            employeesProjectsPositionsService.Verify(epps => epps.RemoveParticipantsAsync(It.IsAny<List<ProjectEditParticipantDto>>()), Times.Once, errorMessagePrefix);
            employeesProjectsPositionsService.Verify(epps => epps.AddParticipantsAsync(It.IsAny<List<ProjectEditParticipantDto>>()), Times.Once, errorMessagePrefix);
        }
    }
}
