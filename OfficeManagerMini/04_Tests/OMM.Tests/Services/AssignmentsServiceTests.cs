using Microsoft.EntityFrameworkCore;
using Moq;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Assignments;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class AssignmentsServiceTests
    {
        #region AssignmentDummyData
        private const string Assignment_Id_1 = "1";
        private const string Assignment_Id_2 = "2";
        private const string Assignment_Id_3 = "3";
        private const string Assignment_Id_4 = "4";
        private const string Assignment_Id_5 = "5";

        private const string Assignment_Name_1 = "Assignment Name 1";
        private const string Assignment_Name_2 = "Assignment Name 2";
        private const string Assignment_Name_3 = "Assignment Name 3";
        private const string Assignment_Name_4 = "Assignment Name 4";
        private const string Assignment_Name_5 = "Assignment Name 5";

        private const string Assignment_Description_1 = "Assignment random text description 1";
        private const string Assignment_Description_2 = "Assignment random text description 2";
        private const string Assignment_Description_3 = "Assignment random text description 3";
        private const string Assignment_Description_4 = "Assignment random text description 4";
        private const string Assignment_Description_5 = "Assignment random text description 5";

        private const string Assignment_Type_1 = "Offer";
        private const string Assignment_Type_2 = "Report";
        private const string Assignment_Type_3 = "Invoice";
        private const string Assignment_Type_4 = "Other";

        private const bool Assignment_IsProjectRelated_YES = true;
        private const bool Assignment_IsProjectRelated_NO = false;

        private const double Assignment_Progress_0 = 0.0;
        private const double Assignment_Progress_255 = 25.5;
        private const double Assignment_Progress_50 = 50.0;
        private const double Assignment_Progress_755 = 75.5;
        private const double Assignment_Progress_100 = 100.0;

        private const int Priority_3 = 3;
        private const int Priority_5 = 5;
        private const int Priority_7 = 7;
        private const int Priority_10 = 10;
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

        private const string Employee_Email_1 = "employee1@ludnica.com";
        private const string Employee_Email_2 = "employee2@ludnica.com";
        private const string Employee_Email_3 = "employee3@ludnica.com";
        private const string Employee_Email_4 = "employee4@ludnica.com";
        private const string Employee_Email_5 = "employee5@ludnica.com";

        private const string Employee_PhoneNumber_1 = "088888888881";
        private const string Employee_PhoneNumber_2 = "088888888882";
        private const string Employee_PhoneNumber_3 = "088888888883";
        private const string Employee_PhoneNumber_4 = "088888888884";
        private const string Employee_PhoneNumber_5 = "088888888885";
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

        #region ProjectsDummyData
        private const string Project_Id_1 = "01";
        private const string Project_Id_2 = "02";

        private const string Project_Name_1 = "Project name 1";
        private const string Project_Name_2 = "Project name 2";
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

        private IAssignmentsService assignmentsService;

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

        private List<Employee> GetEmployeesDummyData()
        {
            return new List<Employee>
            {
                new Employee { Id = Employee_Id_1, FullName = Employee_FullName_1, ProfilePicture = Employee_ProfilePicture_1, Email = Employee_Email_1, PhoneNumber = Employee_PhoneNumber_1, DepartmentId = Department_Id_1},
                new Employee { Id = Employee_Id_2, FullName = Employee_FullName_2, ProfilePicture = Employee_ProfilePicture_2, Email = Employee_Email_2, PhoneNumber = Employee_PhoneNumber_2, DepartmentId = Department_Id_2},
                new Employee { Id = Employee_Id_3, FullName = Employee_FullName_3, ProfilePicture = Employee_ProfilePicture_3, Email = Employee_Email_3, PhoneNumber = Employee_PhoneNumber_3, DepartmentId = Department_Id_2},
                new Employee { Id = Employee_Id_4, FullName = Employee_FullName_4, ProfilePicture = Employee_ProfilePicture_4, Email = Employee_Email_4, PhoneNumber = Employee_PhoneNumber_4, DepartmentId = Department_Id_3},
                new Employee { Id = Employee_Id_5, FullName = Employee_FullName_5, ProfilePicture = Employee_ProfilePicture_5, Email = Employee_Email_5, PhoneNumber = Employee_PhoneNumber_5, DepartmentId = Department_Id_4},
            };
        }

        private List<Project> GetProjectsDummyData()
        {
            return new List<Project>
            {
                new Project { Id = Project_Id_1, Name = Project_Name_1},
                new Project { Id = Project_Id_2, Name = Project_Name_2},
            };
        }

        private List<Assignment> GetAssignmentsDummyData()
        {
            return new List<Assignment>
            {
                new Assignment
                { Id = Assignment_Id_1,
                    Name = Assignment_Name_1,
                    Description = Assignment_Description_1,
                    Type = Assignment_Type_1,
                    IsProjectRelated = Assignment_IsProjectRelated_NO,
                    Progress = Assignment_Progress_0,
                    StartingDate = DateTime.UtcNow.AddDays(-15),
                    StatusId = Status_Id_InProgress,
                    Priority = Priority_3,
                    AssignorId = Employee_Id_1,
                    ExecutorId = Employee_Id_2,
                },
                new Assignment
                { Id = Assignment_Id_2,
                    Name = Assignment_Name_2,
                    Description = Assignment_Description_2,
                    Type = Assignment_Type_2,
                    IsProjectRelated = Assignment_IsProjectRelated_NO,
                    Progress = Assignment_Progress_255,
                    StartingDate = DateTime.UtcNow.AddDays(-10),
                    StatusId = Status_Id_Frozen,
                    Priority = Priority_5,
                    AssignorId = Employee_Id_2,
                    ExecutorId = Employee_Id_3,
                    AssignmentsAssistants = new List<AssignmentsEmployees>
                    {
                        new AssignmentsEmployees{ AssistantId = Employee_Id_1},
                        new AssignmentsEmployees{ AssistantId = Employee_Id_4},
                    }
                },
                new Assignment
                { Id = Assignment_Id_3,
                    Name = Assignment_Name_3,
                    Description = Assignment_Description_3,
                    Type = Assignment_Type_3,
                    IsProjectRelated = Assignment_IsProjectRelated_NO,
                    Progress = Assignment_Progress_100,
                    StartingDate = DateTime.UtcNow.AddDays(-25),
                    StatusId = Status_Id_Completed,
                    EndDate = DateTime.UtcNow.AddDays(-5),
                    Deadline = DateTime.UtcNow.AddDays(-4),
                    Priority = Priority_10,
                    AssignorId = Employee_Id_3,
                    ExecutorId = Employee_Id_2,
                },
                new Assignment
                { Id = Assignment_Id_4,
                    Name = Assignment_Name_4,
                    Description = Assignment_Description_4,
                    Type = Assignment_Type_4,
                    IsProjectRelated = Assignment_IsProjectRelated_YES,
                    ProjectId = Project_Id_1,
                    Progress = Assignment_Progress_50,
                    StartingDate = DateTime.UtcNow.AddDays(-5),
                    StatusId = Status_Id_InProgress,
                    Priority = Priority_7,
                    AssignorId = Employee_Id_1,
                    ExecutorId = Employee_Id_3,
                    AssignmentsAssistants = new List<AssignmentsEmployees>
                    {
                        new AssignmentsEmployees{ AssistantId = Employee_Id_2},
                    },
                    Comments = new List<Comment>
                    {
                        new Comment {Id = "0001", Description = "Random description 1", CreatedOn = DateTime.UtcNow.AddDays(-3), CommentatorId = Employee_Id_3 },
                        new Comment {Id = "0002", Description = "Random description 2", CreatedOn = DateTime.UtcNow.AddDays(-2), CommentatorId = Employee_Id_2 },
                    },
                },
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetStatusesDummyData());
            await context.AddRangeAsync(GetDepartmentsDummyData());
            await context.AddRangeAsync(GetProjectsDummyData());
            await context.SaveChangesAsync();

            await context.AddRangeAsync(GetEmployeesDummyData());
            await context.SaveChangesAsync();

            await context.AddRangeAsync(GetAssignmentsDummyData());
            await context.SaveChangesAsync();
        }

        public AssignmentsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateAssignmentAsync_WithValidDataNotProjectRelatedAndStatusNotCompleted_ShouldCreateAssignmentAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService CreateAssignmentAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var assignmentToCreate = new AssignmentCreateDto
            {
                Name = Assignment_Name_5,
                Description = Assignment_Description_5,
                AssignorId = Employee_Id_5,
                ExecutorId = Employee_Id_4,
                StartingDate = "28-08-2019",
                Priority = Priority_5,
                Progress = Assignment_Progress_0,
                StatusId = Status_Id_InProgress,
                Type = Assignment_Type_1,
                IsProjectRelated = Assignment_IsProjectRelated_NO
            };

            bool actualResult = await this.assignmentsService.CreateAssignmentAsync(assignmentToCreate);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateAssignmentAsync_WithValidDataAndStatustCompleted_ShouldCreateAssignmentAndSetEndDateToDateAndProgressTo100AndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService CreateAssignmentAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var assignmentToCreate = new AssignmentCreateDto
            {
                Name = Assignment_Name_5,
                Description = Assignment_Description_5,
                AssignorId = Employee_Id_5,
                ExecutorId = Employee_Id_4,
                StartingDate = "28-08-2019",
                Priority = Priority_5,
                Progress = Assignment_Progress_0,
                StatusId = Status_Id_Completed,
                Type = Assignment_Type_1,
                IsProjectRelated = Assignment_IsProjectRelated_NO
            };

            bool result = await this.assignmentsService.CreateAssignmentAsync(assignmentToCreate);
            var actualAssignment = await context.Assignments.SingleAsync(a => a.Name == Assignment_Name_5);

            Assert.True(result, errorMessagePrefix);
            Assert.True(actualAssignment.EndDate.HasValue, errorMessagePrefix + " " + "EndDate was not set correctly during the creation!");
            Assert.True(actualAssignment.Progress == Assignment_Progress_100, errorMessagePrefix + " " + "Progress was not set correctly during the creation!");
            Assert.True(actualAssignment.StatusId == Status_Id_Completed, errorMessagePrefix + " " + "StatusId was not set correctly during the creation!");

        }

        [Fact]
        public async Task CreateAssignmentAsync_WithValidDataProjectRelated_ShouldCreateAssignmentAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService CreateAssignmentAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var assignmentToCreate = new AssignmentCreateDto
            {
                Name = Assignment_Name_5,
                Description = Assignment_Description_5,
                AssignorId = Employee_Id_5,
                ExecutorId = Employee_Id_4,
                StartingDate = "28-08-2019",
                Priority = Priority_5,
                Progress = Assignment_Progress_0,
                StatusId = Status_Id_InProgress,
                Type = Assignment_Type_1,
                IsProjectRelated = Assignment_IsProjectRelated_YES,
                ProjectId = Project_Id_2
            };

            bool actualResult = await this.assignmentsService.CreateAssignmentAsync(assignmentToCreate);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateAssignmentAsync_WithValidDataAndAssistants_ShouldCreateAssignmentAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService CreateAssignmentAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var assignmentToCreate = new AssignmentCreateDto
            {
                Name = Assignment_Name_5,
                Description = Assignment_Description_5,
                AssignorId = Employee_Id_5,
                ExecutorId = Employee_Id_4,
                StartingDate = "28-08-2019",
                Priority = Priority_5,
                Progress = Assignment_Progress_0,
                StatusId = Status_Id_InProgress,
                Type = Assignment_Type_1,
                IsProjectRelated = Assignment_IsProjectRelated_NO,
                AssistantsIds = new List<string> { Employee_Id_2, Employee_Id_3 }
            };

            bool actualResult = await this.assignmentsService.CreateAssignmentAsync(assignmentToCreate);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task GetAllAssignments_WithDummyData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllAssignments() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            List<AssignmentListDto> expectedResults = context.Assignments.To<AssignmentListDto>().ToList();
            List<AssignmentListDto> actualResults = this.assignmentsService.GetAllAssignments().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
                Assert.True(expectedEntry.Type == actualEntry.Type, errorMessagePrefix + " " + "Type is not returned properly.");
                Assert.True(expectedEntry.Description == actualEntry.Description, errorMessagePrefix + " " + "Description is not returned properly.");
                Assert.True(expectedEntry.Deadline == actualEntry.Deadline, errorMessagePrefix + " " + "Deadline is not returned properly.");
                Assert.True(expectedEntry.StartingDate == actualEntry.StartingDate, errorMessagePrefix + " " + "StartingDate name is not returned properly.");
                Assert.True(expectedEntry.AssignorName == actualEntry.AssignorName, errorMessagePrefix + " " + "AssignorName is not returned properly.");
                Assert.True(expectedEntry.ExecutorName == actualEntry.ExecutorName, errorMessagePrefix + " " + "ExecutorName is not returned properly.");
                Assert.True(expectedEntry.EndDate == actualEntry.EndDate, errorMessagePrefix + " " + "EndDate is not returned properly.");
                Assert.True(expectedEntry.Priority == actualEntry.Priority, errorMessagePrefix + " " + "Priority is not returned properly.");
                Assert.True(expectedEntry.StatusName == actualEntry.StatusName, errorMessagePrefix + " " + "StatusName is not returned properly.");
                Assert.True(expectedEntry.Progress == actualEntry.Progress, errorMessagePrefix + " " + "Progress is not returned properly.");
                Assert.True(expectedEntry.IsProjectRelated == actualEntry.IsProjectRelated, errorMessagePrefix + " " + "IsProjectRelated is not returned properly.");
            }

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returend assignments is not correct!");
        }

        [Fact]
        public async Task GetAllAssignments_WithZeroData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllAssignments() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            List<AssignmentListDto> actualResults = await this.assignmentsService.GetAllAssignments().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returend assignments is not correct!");
        }

        [Fact]
        public async Task GetAllMyAssignments_WithValidDataIncludingEmployeeAsAssignorAndExecutorAndAssistant_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllMyAssignments() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var expectedResults = GetAssignmentsDummyData()
                .Where(a => a.AssignorId == Employee_Id_1 ||
                            a.ExecutorId == Employee_Id_1 ||
                            a.AssignmentsAssistants.Any(aa => aa.AssistantId == Employee_Id_1))
                .To<AssignmentListDto>()
                .ToList();

            var actualResults = this.assignmentsService.GetAllMyAssignments(Employee_Id_1).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            }

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of retured my assignments is not correct!");
        }

        [Fact]
        public async Task GetAllMyAssignments_WithZeroAssignmentsToEmployee_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllMyAssignments() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var actualResults = this.assignmentsService.GetAllMyAssignments(Employee_Id_5).ToList();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of retured my assignments is not correct!");
        }

        [Fact]
        public async Task GetAllMyAssignments_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            string invalidId = "Invalid id";

            var ex = Assert.Throws<NullReferenceException>(() => this.assignmentsService.GetAllMyAssignments(invalidId).ToList());

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, invalidId), ex.Message);
        }

        [Fact]
        public async Task GetAllAssignmentsForMe_WithValidData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllAssignmentsForMe() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var expectedResults = GetAssignmentsDummyData()
                .Where(a => a.ExecutorId == Employee_Id_2)
                .To<AssignmentListDto>()
                .ToList();

            var actualResults = this.assignmentsService.GetAllAssignmentsForMe(Employee_Id_2).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            }

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of retured my assignments is not correct!");
        }

        [Fact]
        public async Task GetAllAssignmentsForMe_WithZeroAssignmentsToEmployee_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllAssignmentsForMe() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var actualResults = this.assignmentsService.GetAllAssignmentsForMe(Employee_Id_5).ToList();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of retured my assignments is not correct!");
        }

        [Fact]
        public async Task GetAllAssignmentsForMe_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            string invalidId = "Invalid id";

            var ex = Assert.Throws<NullReferenceException>(() => this.assignmentsService.GetAllAssignmentsForMe(invalidId).ToList());

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, invalidId), ex.Message);
        }

        [Fact]
        public async Task GetAllAssignmentsFromMe_WithValidData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllAssignmentsFromMe() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var expectedResults = GetAssignmentsDummyData()
                .Where(a => a.AssignorId == Employee_Id_1)
                .To<AssignmentListDto>()
                .ToList();

            var actualResults = this.assignmentsService.GetAllAssignmentsFromMe(Employee_Id_1).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            }

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of retured my assignments is not correct!");
        }

        [Fact]
        public async Task GetAllAssignmentsFromMe_WithZeroAssignmentsToEmployee_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllAssignmentsFromMe() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var actualResults = this.assignmentsService.GetAllAssignmentsFromMe(Employee_Id_5).ToList();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of retured my assignments is not correct!");
        }

        [Fact]
        public async Task GetAllAssignmentsFromMe_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            string invalidId = "Invalid id";

            var ex = Assert.Throws<NullReferenceException>(() => this.assignmentsService.GetAllAssignmentsFromMe(invalidId).ToList());

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, invalidId), ex.Message);
        }

        [Fact]
        public async Task GetAllAssignmentsAsAssistant_WithValidData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllAssignmentsAsAssistant() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var expectedResults = GetAssignmentsDummyData()
                .Where(a => a.AssignmentsAssistants.Any(aa => aa.AssistantId == Employee_Id_1))
                .To<AssignmentListDto>()
                .ToList();

            var actualResults = this.assignmentsService.GetAllAssignmentsAsAssistant(Employee_Id_1).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            }

            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of retured my assignments is not correct!");
        }

        [Fact]
        public async Task GetAllAssignmentsAsAssistant_WithZeroAssignmentsToEmployee_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentsService GetAllAssignmentsAsAssistant() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var actualResults = this.assignmentsService.GetAllAssignmentsAsAssistant(Employee_Id_5).ToList();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of retured my assignments is not correct!");
        }

        [Fact]
        public async Task GetAllAssignmentsAsAssistant_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            string invalidId = "Invalid id";

            var ex = Assert.Throws<NullReferenceException>(() => this.assignmentsService.GetAllAssignmentsAsAssistant(invalidId).ToList());

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, invalidId), ex.Message);
        }

        [Theory]
        [InlineData(Assignment_Id_1)]
        [InlineData(Assignment_Id_2)]
        [InlineData(Assignment_Id_3)]
        [InlineData(Assignment_Id_4)]
        public async Task GetAssignmentDetails_WithValidData_ShouldReturnCorrectResult(string assignmentId)
        {
            string errorMessagePrefix = "AssignmentsService GetAssignmentDetails() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentDetailsDto expectedResult = (await context.Assignments.SingleAsync(a => a.Id == assignmentId)).To<AssignmentDetailsDto>();
            AssignmentDetailsDto actualResult = await this.assignmentsService.GetAssignmentDetails(assignmentId).SingleAsync();

            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not returned properly.");
            Assert.True(expectedResult.ProjectName == actualResult.ProjectName, errorMessagePrefix + " " + "ProjectName is not returned properly.");
            Assert.True(expectedResult.Priority == actualResult.Priority, errorMessagePrefix + " " + "Priority is not returned properly.");
            Assert.True(expectedResult.AssignorEmail == actualResult.AssignorEmail, errorMessagePrefix + " " + "AssignorEmail is not returned properly.");
            Assert.True(expectedResult.AssignorFullName == actualResult.AssignorFullName, errorMessagePrefix + " " + "AssignorFullName is not returned properly.");
            Assert.True(expectedResult.ExecutorEmail == actualResult.ExecutorEmail, errorMessagePrefix + " " + "ExecutorEmail is not returned properly.");
            Assert.True(expectedResult.ExecutorFullName == actualResult.ExecutorFullName, errorMessagePrefix + " " + "ExecutorFullName is not returned properly.");
            Assert.True(expectedResult.ExecutorPhone == actualResult.ExecutorPhone, errorMessagePrefix + " " + "ExecutorPhone is not returned properly.");
            Assert.True(expectedResult.ExecutorProfilePicture == actualResult.ExecutorProfilePicture, errorMessagePrefix + " " + "ExecutorProfilePicture is not returned properly.");
            Assert.True(expectedResult.StartingDate == actualResult.StartingDate, errorMessagePrefix + " " + "StartingDate is not returned properly.");
            Assert.True(expectedResult.Type == actualResult.Type, errorMessagePrefix + " " + "Type is not returned properly.");
            Assert.True(expectedResult.ChangeData.Deadline == actualResult.ChangeData.Deadline, errorMessagePrefix + " " + "Deadline is not returned properly.");
            Assert.True(expectedResult.ChangeData.EndDate == actualResult.ChangeData.EndDate, errorMessagePrefix + " " + "EndDate is not returned properly.");
            Assert.True(expectedResult.ChangeData.StatusId == actualResult.ChangeData.StatusId, errorMessagePrefix + " " + "StatusId is not returned properly.");
            Assert.True(expectedResult.ChangeData.StatusName == actualResult.ChangeData.StatusName, errorMessagePrefix + " " + "StatusName is not returned properly.");
            Assert.True(expectedResult.ChangeData.Progress == actualResult.ChangeData.Progress, errorMessagePrefix + " " + "Progress is not returned properly.");
            Assert.True(expectedResult.Assistants.Count == actualResult.Assistants.Count, errorMessagePrefix + " " + "Assistants are not returned properly.");
            Assert.True(expectedResult.Comments.Count == actualResult.Comments.Count, errorMessagePrefix + " " + "Comments are not returned properly.");
        }

        [Fact]
        public async Task GetAssignmentDetails_WithInvalidAssignmentId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            string invalidId = "Invalid id";

            var ex = Assert.Throws<NullReferenceException>(() => this.assignmentsService.GetAssignmentDetails(invalidId));

            Assert.Equal(string.Format(ErrorMessages.AssignmentIdNullReference, invalidId), ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_WithValidData_ShouldDeleteAssingmentAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService DeleteAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var assignmentToDeleteId = (await context.Assignments.FirstAsync()).Id;
            var expectedCount = context.Assignments.Count() - 1;

            bool actualResult = await this.assignmentsService.DeleteAsync(assignmentToDeleteId);
            var actualCount = context.Assignments.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.True(expectedCount == actualCount, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidAssignmentId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            string invalidId = "Invalid id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assignmentsService.DeleteAsync(invalidId));

            Assert.Equal(string.Format(ErrorMessages.AssignmentIdNullReference, invalidId), ex.Message);
        }

        [Fact]
        public async Task DeleteProjectAssignmentsAsync_WithValidData_ShouldDeleteAssignmentsAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService DeleteProjectAssignmentsAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            List<Assignment> assignmentsToDelete = context.Assignments.Where(a => a.ProjectId == Project_Id_1).ToList();
            var assignmentsToDeleteCount = assignmentsToDelete.Count;
            var expectedCount = context.Assignments.Count() - assignmentsToDeleteCount;

            bool actualResult = await this.assignmentsService.DeleteProjectAssignmentsAsync(assignmentsToDelete);
            var actualCount = context.Assignments.Count();

            Assert.True(actualResult, errorMessagePrefix);
            Assert.True(expectedCount == actualCount, errorMessagePrefix);
        }

        [Theory]
        [InlineData(Assignment_Id_1)]
        [InlineData(Assignment_Id_2)]
        [InlineData(Assignment_Id_3)]
        [InlineData(Assignment_Id_4)]
        public async Task GetAssignmentToEditAsync_WithValidData_ShouldReturnCorrectResult(string assignmentId)
        {
            string errorMessagePrefix = "AssignmentsService GetAssignmentToEditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.SingleAsync(a => a.Id == assignmentId)).To<AssignmentEditDto>();
            AssignmentEditDto actualResult = await this.assignmentsService.GetAssignmentToEditAsync(assignmentId);

            Assert.True(expectedResult.Id == actualResult.Id, errorMessagePrefix + " " + "Id is not returned properly.");
            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not returned properly.");
            Assert.True(expectedResult.Priority == actualResult.Priority, errorMessagePrefix + " " + "Priority is not returned properly.");
            Assert.True(expectedResult.Progress == actualResult.Progress, errorMessagePrefix + " " + "Progress is not returned properly.");
            Assert.True(expectedResult.IsProjectRelated == actualResult.IsProjectRelated, errorMessagePrefix + " " + "IsProjectRelated is not returned properly.");
            Assert.True(expectedResult.Type == actualResult.Type, errorMessagePrefix + " " + "Type is not returned properly.");
            Assert.True(expectedResult.ProjectId == actualResult.ProjectId, errorMessagePrefix + " " + "ProjectId is not returned properly.");
            Assert.True(expectedResult.StartingDate == actualResult.StartingDate, errorMessagePrefix + " " + "StartingDate is not returned properly.");
            Assert.True(expectedResult.ExecutorId == actualResult.ExecutorId, errorMessagePrefix + " " + "ExecutorId is not returned properly.");
            Assert.True(expectedResult.AssignorName == actualResult.AssignorName, errorMessagePrefix + " " + "AssignorName is not returned properly.");
            Assert.True(expectedResult.Deadline == actualResult.Deadline, errorMessagePrefix + " " + "Deadline is not returned properly.");
            Assert.True(expectedResult.AssistantsIds.Count == actualResult.AssistantsIds.Count, errorMessagePrefix + " " + "Assistants are not returned properly.");
        }

        [Fact]
        public async Task GetAssignmentToEditAsync_WithInvalidAssignmentId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            string invalidId = "Invalid id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assignmentsService.GetAssignmentToEditAsync(invalidId));

            Assert.Equal(string.Format(ErrorMessages.AssignmentIdNullReference, invalidId), ex.Message);
        }

        [Fact]
        public async Task EditAsync_WithCommonDataWithoutIfStatements_ShouldEditAssignment()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1)).To<AssignmentEditDto>();

            expectedResult.ExecutorId = Employee_Id_3;
            expectedResult.StartingDate = "28-08-2019";
            expectedResult.Priority = Priority_10;
            expectedResult.Type = Assignment_Type_3;
            expectedResult.Name = Assignment_Name_5;
            expectedResult.Description = Assignment_Description_5;

            await this.assignmentsService.EditAsync(expectedResult);

            AssignmentEditDto actualResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1)).To<AssignmentEditDto>();

            Assert.True(expectedResult.Name == actualResult.Name, errorMessagePrefix + " " + "Name is not changed properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not changed properly.");
            Assert.True(expectedResult.Priority == actualResult.Priority, errorMessagePrefix + " " + "Priority is not changed properly.");
            Assert.True(expectedResult.Type == actualResult.Type, errorMessagePrefix + " " + "Type is not changed properly.");
            Assert.True(expectedResult.StartingDate == actualResult.StartingDate, errorMessagePrefix + " " + "StartingDate is not changed properly.");
            Assert.True(expectedResult.ExecutorId == actualResult.ExecutorId, errorMessagePrefix + " " + "ExecutorId is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_ChangeDeadlineFromNullToActualDate_ShouldEditAssignmentCorrectly()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1)).To<AssignmentEditDto>();

            expectedResult.Deadline = "28-08-2019";

            await this.assignmentsService.EditAsync(expectedResult);

            AssignmentEditDto actualResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1)).To<AssignmentEditDto>();

            Assert.True(expectedResult.Deadline == actualResult.Deadline, errorMessagePrefix + " " + "Deadline is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_ChangeDeadlineFromDateToNull_ShouldEditAssignmentCorrectly()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_3)).To<AssignmentEditDto>();

            expectedResult.Deadline = "";

            await this.assignmentsService.EditAsync(expectedResult);

            AssignmentEditDto actualResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_3)).To<AssignmentEditDto>();

            Assert.True(expectedResult.Deadline == actualResult.Deadline, errorMessagePrefix + " " + "Deadline is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_ChangeIsProjectRelateFromFalseToTrue_ShouldEditAssignmentCorrectly()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1)).To<AssignmentEditDto>();

            expectedResult.IsProjectRelated = Assignment_IsProjectRelated_YES;
            expectedResult.ProjectId = Project_Id_2;

            await this.assignmentsService.EditAsync(expectedResult);

            AssignmentEditDto actualResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1)).To<AssignmentEditDto>();

            Assert.True(expectedResult.IsProjectRelated == actualResult.IsProjectRelated, errorMessagePrefix + " " + "IsProjectRelated is not changed properly.");
            Assert.True(expectedResult.ProjectId == actualResult.ProjectId, errorMessagePrefix + " " + "ProjectId is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_ChangeIsProjectRelateFromTrueToFalse_ShouldEditAssignmentCorrectly()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_4)).To<AssignmentEditDto>();

            expectedResult.IsProjectRelated = Assignment_IsProjectRelated_NO;
            expectedResult.ProjectId = null;

            await this.assignmentsService.EditAsync(expectedResult);

            AssignmentEditDto actualResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_4)).To<AssignmentEditDto>();

            Assert.True(expectedResult.IsProjectRelated == actualResult.IsProjectRelated, errorMessagePrefix + " " + "IsProjectRelated is not changed properly.");
            Assert.True(expectedResult.ProjectId == actualResult.ProjectId, errorMessagePrefix + " " + "ProjectId is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_ChangeStatusFromNotCompletedToNotCompleted_ShouldEditAssignmentCorrectly()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1)).To<AssignmentEditDto>();

            expectedResult.StatusId = Status_Id_Waiting;
            expectedResult.Progress = Assignment_Progress_50;

            await this.assignmentsService.EditAsync(expectedResult);

            Assignment actualResult = await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1);

            Assert.True(expectedResult.StatusId == actualResult.StatusId, errorMessagePrefix + " " + "StatusId is not changed properly.");
            Assert.True(expectedResult.Progress == actualResult.Progress, errorMessagePrefix + " " + "Progress is not changed properly.");
            Assert.True(actualResult.EndDate == null, errorMessagePrefix + " " + "EndDate is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_ChangeStatusFromNotCompletedToCompleted_ShouldEditAssignmentCorrectly()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_Completed);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1)).To<AssignmentEditDto>();

            expectedResult.StatusId = Status_Id_Completed;

            await this.assignmentsService.EditAsync(expectedResult);

            Assignment actualResult = await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_1);

            Assert.True(expectedResult.StatusId == actualResult.StatusId, errorMessagePrefix + " " + "StatusId is not changed properly.");
            Assert.True(actualResult.Progress == Assignment_Progress_100, errorMessagePrefix + " " + "Progress is not changed properly.");
            Assert.True(actualResult.EndDate != null, errorMessagePrefix + " " + "EndDate is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_ChangeStatusFromCompletedToNoTCompleted_ShouldEditAssignmentCorrectly()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_3)).To<AssignmentEditDto>();

            expectedResult.StatusId = Status_Id_InProgress;
            expectedResult.Progress = Assignment_Progress_755;

            await this.assignmentsService.EditAsync(expectedResult);

            Assignment actualResult = await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_3);

            Assert.True(expectedResult.StatusId == actualResult.StatusId, errorMessagePrefix + " " + "StatusId is not changed properly.");
            Assert.True(expectedResult.Progress == actualResult.Progress, errorMessagePrefix + " " + "Progress is not changed properly.");
            Assert.True(actualResult.EndDate == null, errorMessagePrefix + " " + "EndDate is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_DontChangeAssistants_ShouldNotCallAssignmentsEmployeesService()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();
            
            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_2)).To<AssignmentEditDto>();

            expectedResult.Description = Assignment_Description_5;

            await this.assignmentsService.EditAsync(expectedResult);

            Assignment actualResult = await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_2);

            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not changed properly.");
            assignmentsEmployeesService.Verify(ae => ae.AddAssistantsAsync(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Never, errorMessagePrefix);
            assignmentsEmployeesService.Verify(ae => ae.RemoveAssistantsAsync(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Never, errorMessagePrefix);
        }

        [Fact]
        public async Task EditAsync_AddAssistants_ShouldCallAddAssistantsAsyncFromAssignmentsEmployeesService()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_2)).To<AssignmentEditDto>();

            List<string> assistantsToAdd = new List<string> { Employee_Id_5 };

            expectedResult.AssistantsIds.AddRange(assistantsToAdd);

            await this.assignmentsService.EditAsync(expectedResult);

            Assignment actualResult = await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_2);

            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not changed properly.");
            assignmentsEmployeesService.Verify(ae => ae.AddAssistantsAsync(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Once, errorMessagePrefix);
            assignmentsEmployeesService.Verify(ae => ae.RemoveAssistantsAsync(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Never, errorMessagePrefix);
        }

        [Fact]
        public async Task EditAsync_RemoveAssistants_ShouldCallRemoveAssistantsAsyncFromAssignmentsEmployeesService()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_2)).To<AssignmentEditDto>();

            List<string> assistantsToRemove = new List<string> { Employee_Id_1, Employee_Id_4 };

            for (int i = 0; i < assistantsToRemove.Count; i++)
            {
                expectedResult.AssistantsIds.Remove(assistantsToRemove[i]);
            }

            await this.assignmentsService.EditAsync(expectedResult);

            Assignment actualResult = await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_2);

            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not changed properly.");
            assignmentsEmployeesService.Verify(ae => ae.AddAssistantsAsync(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Never, errorMessagePrefix);
            assignmentsEmployeesService.Verify(ae => ae.RemoveAssistantsAsync(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Once, errorMessagePrefix);
        }

        [Fact]
        public async Task EditAsync_AddAndRemoveAssistants_ShouldCallAssignmentsEmployeesService()
        {
            string errorMessagePrefix = "AssignmentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Status_InProgress);

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentEditDto expectedResult = (await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_2)).To<AssignmentEditDto>();

            List<string> assistantsToAdd = new List<string> { Employee_Id_5 };

            expectedResult.AssistantsIds.AddRange(assistantsToAdd);

            List<string> assistantsToRemove = new List<string> { Employee_Id_1, Employee_Id_4 };

            for (int i = 0; i < assistantsToRemove.Count; i++)
            {
                expectedResult.AssistantsIds.Remove(assistantsToRemove[i]);
            }

            await this.assignmentsService.EditAsync(expectedResult);

            Assignment actualResult = await context.Assignments.FirstAsync(a => a.Id == Assignment_Id_2);

            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not changed properly.");
            assignmentsEmployeesService.Verify(ae => ae.AddAssistantsAsync(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Once, errorMessagePrefix);
            assignmentsEmployeesService.Verify(ae => ae.RemoveAssistantsAsync(It.IsAny<List<string>>(), It.IsAny<string>()), Times.Once, errorMessagePrefix);
        }

        [Fact]
        public async Task ChangeDataAsync_WithInvalidAssignmnentId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            var input = (await context.Assignments.FirstAsync()).To<AssignmentDetailsChangeDto>();

            string invalidId = "Invlaid id";

            input.Id = invalidId;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assignmentsService.ChangeDataAsync(input));
            Assert.Equal(string.Format(ErrorMessages.AssignmentIdNullReference, invalidId), ex.Message);
        }

        [Fact]
        public async Task ChangeDataAsync_ChangeDeadlineFromNullToDate_ShouldChangeAssignmentCorrectlyAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) => {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentDetailsChangeDto expectedResult = (await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_1)).To<AssignmentDetailsChangeDto>();

            expectedResult.Deadline = "30-08-2019";

            bool result = await this.assignmentsService.ChangeDataAsync(expectedResult);

            AssignmentDetailsChangeDto actualResult = (await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_1)).To<AssignmentDetailsChangeDto>();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedResult.Deadline == actualResult.Deadline, errorMessagePrefix + " " + "Deadline is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_ChangeDeadlineFromDateToNull_ShouldChangeAssignmentCorrectlyAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) => {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentDetailsChangeDto expectedResult = (await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_3)).To<AssignmentDetailsChangeDto>();

            expectedResult.Deadline = null;

            bool result = await this.assignmentsService.ChangeDataAsync(expectedResult);

            Assignment actualResult = await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_3);

            Assert.True(result, errorMessagePrefix);
            Assert.False(actualResult.Deadline.HasValue, errorMessagePrefix + " " + "Deadline is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_SetOnlyEndDateFromNullToDateWhenStatusIsNotCompleted_ShouldChangeStatusToCompletedAndProgressTo100AndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) => {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentDetailsChangeDto expectedResult = (await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_1)).To<AssignmentDetailsChangeDto>();

            expectedResult.EndDate = "30-08-2019";

            bool result = await this.assignmentsService.ChangeDataAsync(expectedResult);

            AssignmentDetailsChangeDto actualResult = (await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_1)).To<AssignmentDetailsChangeDto>();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedResult.EndDate == actualResult.EndDate , errorMessagePrefix + " " + "EndDate is not changed correctly!");
            Assert.True(actualResult.StatusId == Status_Id_Completed , errorMessagePrefix + " " + "StatusId is not changed correctly!");
            Assert.True(actualResult.StatusName == Status_Completed, errorMessagePrefix + " " + "StatusName is not changed correctly!");
            Assert.True(actualResult.Progress == Assignment_Progress_100, errorMessagePrefix + " " + "Progress is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_SetOnlyEndDateFromDateToNullWhenStatusIsCompleted_ShouldChangeStatusToInProgressAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) => {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentDetailsChangeDto expectedResult = (await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_3)).To<AssignmentDetailsChangeDto>();

            expectedResult.EndDate = null;

            bool result = await this.assignmentsService.ChangeDataAsync(expectedResult);

            Assignment actualResult = await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_3);

            Assert.True(result, errorMessagePrefix);
            Assert.False(actualResult.EndDate.HasValue, errorMessagePrefix + " " + "EndDate is not changed correctly!");
            Assert.True(actualResult.StatusId == Status_Id_InProgress, errorMessagePrefix + " " + "StatusId is not changed correctly!");
            Assert.True(expectedResult.Progress == actualResult.Progress, errorMessagePrefix + " " + "Progress is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_SetOnlyStatusToCompletedWithNoEndDate_ShouldSetEndDateToDateAndProgressTo100AndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) => {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentDetailsChangeDto expectedResult = (await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_1)).To<AssignmentDetailsChangeDto>();

            expectedResult.StatusId = Status_Id_Completed;
            expectedResult.StatusName = Status_Completed;

            bool result = await this.assignmentsService.ChangeDataAsync(expectedResult);

            Assignment actualResult = await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_1);

            Assert.True(result, errorMessagePrefix);
            Assert.True(actualResult.EndDate.HasValue, errorMessagePrefix + " " + "EndDate is not changed correctly!");
            Assert.True(expectedResult.StatusId == actualResult.StatusId, errorMessagePrefix + " " + "StatusId is not changed correctly!");
            Assert.True(actualResult.Progress == Assignment_Progress_100, errorMessagePrefix + " " + "Progress is not changed correctly!");
        }

        [Fact]
        public async Task ChangeDataAsync_SetStatusFromNotCompletedToNotCompletedAndChangeProgress_ShouldChangeStatusAndProgressCorrectlyAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentsService ChangeDataAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var statusService = new Mock<IStatusesService>();
            statusService.Setup(ss => ss.GetStatusNameByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((int id) => {
                             if (id == Status_Id_InProgress) return Status_InProgress;
                             else if (id == Status_Id_Waiting) return Status_Waiting;
                             else if (id == Status_Id_Frozen) return Status_Frozen;
                             else if (id == Status_Id_Delayed) return Status_Delayed;
                             else return Status_Completed;
                         });

            statusService.Setup(ss => ss.GetStatusIdByNameAsync(It.IsAny<string>()))
                         .ReturnsAsync((string statusName) => {
                             if (statusName == Status_InProgress) return Status_Id_InProgress;
                             else if (statusName == Status_Waiting) return Status_Id_Waiting;
                             else if (statusName == Status_Frozen) return Status_Id_Frozen;
                             else if (statusName == Status_Delayed) return Status_Id_Delayed;
                             else return Status_Id_Completed;
                         });

            var assignmentsEmployeesService = new Mock<IAssignmentsEmployeesService>();

            this.assignmentsService = new AssignmentsService(context, statusService.Object, assignmentsEmployeesService.Object);

            AssignmentDetailsChangeDto expectedResult = (await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_1)).To<AssignmentDetailsChangeDto>();

            expectedResult.StatusId = Status_Id_Frozen;
            expectedResult.StatusName = Status_Frozen;
            expectedResult.Progress = Assignment_Progress_50;

            bool result = await this.assignmentsService.ChangeDataAsync(expectedResult);

            AssignmentDetailsChangeDto actualResult = (await context.Assignments.SingleAsync(a => a.Id == Assignment_Id_1)).To<AssignmentDetailsChangeDto>();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedResult.StatusId == actualResult.StatusId, errorMessagePrefix + " " + "StatusId is not changed correctly!");
            Assert.True(expectedResult.StatusName == actualResult.StatusName, errorMessagePrefix + " " + "StatusName is not changed correctly!");
            Assert.True(expectedResult.Progress == actualResult.Progress, errorMessagePrefix + " " + "Progress is not changed correctly!");
        }
    }
}
