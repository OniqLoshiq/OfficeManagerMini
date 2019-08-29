using OMM.Data;
using OMM.Domain;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class AssignmentEmployeesServiceTests
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

        private const string Assignment_Id_1 = "01";
        private const string Assignment_Id_2 = "02";

        private const string Assignment_Name_1 = "Assignment name 1";
        private const string Assignment_Name_2 = "Assignment name 2";

        private IAssignmentsEmployeesService assignmentsEmployeesService;

        private List<AssignmentsEmployees> GetAssignmentsEmployeesDummyData()
        {
            return new List<AssignmentsEmployees>
            {
                new AssignmentsEmployees { AssignmentId = Assignment_Id_1, AssistantId = Employee_Id_1},
                new AssignmentsEmployees { AssignmentId = Assignment_Id_1, AssistantId = Employee_Id_2},
                new AssignmentsEmployees { AssignmentId = Assignment_Id_2, AssistantId = Employee_Id_3},
            };
        }

        private List<Employee> GetEmployeesDummyData()
        {
            return new List<Employee>
            {
                new Employee { Id = Employee_Id_1, FullName =  Employee_FullName_1},
                new Employee { Id = Employee_Id_2, FullName =  Employee_FullName_2},
                new Employee { Id = Employee_Id_3, FullName =  Employee_FullName_3},
                new Employee { Id = Employee_Id_4, FullName =  Employee_FullName_4},
                new Employee { Id = Employee_Id_5, FullName =  Employee_FullName_5},
            };
        }

        private List<Assignment> GetAssignmentsDummyData()
        {
            return new List<Assignment>
            {
                new Assignment { Id = Assignment_Id_1, Name =  Assignment_Name_1},
                new Assignment { Id = Assignment_Id_2, Name =  Assignment_Name_2},
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetEmployeesDummyData());
            await context.AddRangeAsync(GetAssignmentsDummyData());
            await context.SaveChangesAsync();

            await context.AddRangeAsync(GetAssignmentsEmployeesDummyData());
            await context.SaveChangesAsync();
        }

        public AssignmentEmployeesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task AddAssistantsAsync_WithValidData_ShouldAddAssistantsToAssignmentAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentEmployeesService AddAssistantsAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assignmentsEmployeesService = new AssignmentsEmployeesService(context);

            List<string> employeesIdsToAddToAssignment = new List<string> { Employee_Id_3, Employee_Id_4 };

            var expectedCount = context.AssignmentsEmployees.Count() + employeesIdsToAddToAssignment.Count;

            var result = await this.assignmentsEmployeesService.AddAssistantsAsync(employeesIdsToAddToAssignment, Assignment_Id_1);

            var actualCount = context.AssignmentsEmployees.Count();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedCount == actualCount, errorMessagePrefix + " " + "Assistants were not added correctly!");
        }

        [Fact]
        public async Task AddAssistantsAsync_WithInvalidAssignmentId_ShouldThowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assignmentsEmployeesService = new AssignmentsEmployeesService(context);

            List<string> employeesIdsToAddToAssignment = new List<string> { Employee_Id_3, Employee_Id_4 };

            string invalidAssignmentId = "Invalid id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assignmentsEmployeesService.AddAssistantsAsync(employeesIdsToAddToAssignment, invalidAssignmentId));

            Assert.Equal(string.Format(ErrorMessages.AssignmentIdNullReference, invalidAssignmentId), ex.Message);
        }

        [Fact]
        public async Task AddAssistantsAsync_WithInvalidAssistantId_ShouldThowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assignmentsEmployeesService = new AssignmentsEmployeesService(context);

            string invalidEmployeeId = "Invalid id";

            List<string> employeesIdsToAddToAssignment = new List<string> { Employee_Id_3, invalidEmployeeId };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assignmentsEmployeesService.AddAssistantsAsync(employeesIdsToAddToAssignment, Assignment_Id_1));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, invalidEmployeeId), ex.Message);
        }

        [Fact]
        public async Task AddAssistantsAsync_WithExistendAssistantId_ShouldThowArgumentException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assignmentsEmployeesService = new AssignmentsEmployeesService(context);

            List<string> employeesIdsToAddToAssignment = new List<string> { Employee_Id_3, Employee_Id_1 };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => this.assignmentsEmployeesService.AddAssistantsAsync(employeesIdsToAddToAssignment, Assignment_Id_1));

            Assert.Equal(ErrorMessages.AssistantArgumentException, ex.Message);
        }

        [Fact]
        public async Task CreateWithAssistantsIds_WithValidData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "AssignmentEmployeesService CreateWithAssistantsIds() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assignmentsEmployeesService = new AssignmentsEmployeesService(context);

            List<string> employeesIdsToCreateAssignmentWith = new List<string> { Employee_Id_3, Employee_Id_4 };

            var expectedCount = employeesIdsToCreateAssignmentWith.Count;

            List<AssignmentsEmployees> actualResult = this.assignmentsEmployeesService.CreateWithAssistantsIds(employeesIdsToCreateAssignmentWith).ToList();

            var actualCount = actualResult.Count;

            for (int i = 0; i < expectedCount; i++)
            {
                Assert.True(employeesIdsToCreateAssignmentWith[i] == actualResult[i].AssistantId, errorMessagePrefix + " " + "Assistant not created correctly!");
            }
            Assert.True(expectedCount == actualCount, errorMessagePrefix + " " + "Assistants were not added correctly!");
        }

        [Fact]
        public async Task CreateWithAssistantsIds_WithInvalidAssistantId_ShouldThowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assignmentsEmployeesService = new AssignmentsEmployeesService(context);

            string invalidId = "Invalid id";

            List<string> employeesIdsToCreateAssignmentWith = new List<string> { invalidId, Employee_Id_4 };

            var ex = Assert.Throws<NullReferenceException>(() => this.assignmentsEmployeesService.CreateWithAssistantsIds(employeesIdsToCreateAssignmentWith));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, invalidId), ex.Message);
        }

        [Fact]
        public async Task RemoveAssistantsAsync_WithValidData_ShouldRemoveAssistantsFromAssignmentAndReturnTrue()
        {
            string errorMessagePrefix = "AssignmentEmployeesService RemoveAssistantsAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assignmentsEmployeesService = new AssignmentsEmployeesService(context);

            List<string> employeesIdsToRemoveFromAssignment = new List<string> { Employee_Id_1, Employee_Id_2 };

            var expectedCount = context.AssignmentsEmployees.Count() - employeesIdsToRemoveFromAssignment.Count;

            var result = await this.assignmentsEmployeesService.RemoveAssistantsAsync(employeesIdsToRemoveFromAssignment, Assignment_Id_1);

            var actualCount = context.AssignmentsEmployees.Count();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedCount == actualCount, errorMessagePrefix + " " + "Assistants were not removed correctly!");
        }

        [Fact]
        public async Task RemoveAssistantsAsync_WithInvalidAssignmentId_ShouldThowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assignmentsEmployeesService = new AssignmentsEmployeesService(context);

            List<string> employeesIdsToRemoveFromAssignment = new List<string> { Employee_Id_1, Employee_Id_2 };

            string invalidAssignmentId = "Invalid id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assignmentsEmployeesService.RemoveAssistantsAsync(employeesIdsToRemoveFromAssignment, invalidAssignmentId));

            Assert.Equal(string.Format(ErrorMessages.AssignmentIdNullReference, invalidAssignmentId), ex.Message);
        }

        [Fact]
        public async Task RemoveAssistantsAsync_WithInvalidAssistantId_ShouldThowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assignmentsEmployeesService = new AssignmentsEmployeesService(context);

            string invalidAssistantId = "Invalid id";

            List<string> employeesIdsToRemoveFromAssignment = new List<string> { invalidAssistantId, Employee_Id_2 };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assignmentsEmployeesService.RemoveAssistantsAsync(employeesIdsToRemoveFromAssignment, Assignment_Id_1));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, invalidAssistantId), ex.Message);
        }
    }
}
