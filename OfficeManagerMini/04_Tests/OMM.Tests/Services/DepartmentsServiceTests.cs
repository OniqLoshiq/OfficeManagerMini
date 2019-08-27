using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Departments;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class DepartmentsServiceTests
    {

        private const string Department_ManagementBoard_Name = "Management board";
        private const int Department_ManagementBoard_Id = 1;

        private const string Department_HR_Name = "HR";
        private const int Department_HR_Id = 2;

        private const string Department_Accounting_Name = "Accounting";
        private const int Department_Accounting_Id = 3;

        private const string Department_Engineering_Name = "Engineering";
        private const int Department_Engineering_Id = 4;

        private const string Department_Administration_Name = "Administration";
        private const int Department_Administration_Id = 5;

        private const int Department_Invalid_Id1 = 0;
        private const int Department_Invalid_Id2 = -2;
        private const int Department_Invalid_Id3 = 18;


        private IDepartmentsService departmentsService;

        private List<Department> GetDummyData()
        {
            return new List<Department>
            {
                new Department{ Id = Department_ManagementBoard_Id, Name = Department_ManagementBoard_Name},
                new Department{ Id = Department_HR_Id, Name = Department_HR_Name},
                new Department{ Id = Department_Accounting_Id, Name = Department_Accounting_Name},
                new Department{ Id = Department_Engineering_Id, Name = Department_Engineering_Name},
                new Department{ Id = Department_Administration_Id, Name = Department_Administration_Name},
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyData());
            await context.SaveChangesAsync();
        }

        public DepartmentsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetAllDepartmentsByDto_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "Departments GetAllDepartmentsByDto() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.departmentsService = new DepartmentsService(context);

            List<DepartmentListDto> actualResults = await this.departmentsService.GetAllDepartmentsByDto<DepartmentListDto>().ToListAsync();
            List<DepartmentListDto> expectedResults = GetDummyData().To<DepartmentListDto>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            }
            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returned departments is not correct");
        }

        [Fact]
        public async Task GetAllDepartmentsByDto_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "Departments GetAllDepartmentsByDto() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            this.departmentsService = new DepartmentsService(context);

            List<DepartmentListDto> actualResults = await this.departmentsService.GetAllDepartmentsByDto<DepartmentListDto>().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returned departments is not correct");
        }

        [Theory]
        [InlineData(Department_ManagementBoard_Name, Department_ManagementBoard_Id)]
        [InlineData(Department_HR_Name, Department_HR_Id)]
        [InlineData(Department_Accounting_Name, Department_Accounting_Id)]
        [InlineData(Department_Engineering_Name, Department_Engineering_Id)]
        [InlineData(Department_Administration_Name, Department_Administration_Id)]
        public async Task GetDepartmentNameByIdAsync_ShouldReturnCorrectResult(string expectedName, int departmentId)
        {
            string errorMessagePrefix = "ProjectPositions GetProjectPositionNameByIdAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.departmentsService = new DepartmentsService(context);

            var actualName = await this.departmentsService.GetDepartmentNameByIdAsync(departmentId);

            Assert.True(expectedName == actualName, errorMessagePrefix + " " + "The project position name is not correct");
        }

        [Theory]
        [InlineData(Department_Invalid_Id1)]
        [InlineData(Department_Invalid_Id2)]
        [InlineData(Department_Invalid_Id3)]
        public async Task GetDepartmentNameByIdAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException(int id)
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.departmentsService = new DepartmentsService(context);

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => this.departmentsService.GetDepartmentNameByIdAsync(id));

            Assert.Equal(string.Format(ErrorMessages.DepartmentInvalidRange, id), ex.Message);
        }
    }
}
