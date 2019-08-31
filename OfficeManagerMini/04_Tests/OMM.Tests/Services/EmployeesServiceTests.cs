using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Employees;
using OMM.Services.SendGrid;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class EmployeesServiceTests
    {
        #region EmployeesDummyData
        private const string Employee_Id_1 = "001";
        private const string Employee_Id_2 = "002";
        private const string Employee_Id_3 = "003";
        private const string Employee_Id_4 = "004";
        private const string Employee_Id_5 = "005";

        private const string Employee_FirstName_1 = "Test 1";
        private const string Employee_FirstName_2 = "Test 2";
        private const string Employee_FirstName_3 = "Test 3";
        private const string Employee_FirstName_4 = "Test 4";
        private const string Employee_FirstName_5 = "Test 5";

        private const string Employee_MiddleName_1 = "Test name 1";
        private const string Employee_MiddleName_2 = "Test name 2";
        private const string Employee_MiddleName_3 = "Test name 3";
        private const string Employee_MiddleName_4 = "Test name 4";
        private const string Employee_MiddleName_5 = "Test name 5";

        private const string Employee_LastName_1 = "Test empl name 1";
        private const string Employee_LastName_2 = "Test empl name 2";
        private const string Employee_LastName_3 = "Test empl name 3";
        private const string Employee_LastName_4 = "Test empl name 4";
        private const string Employee_LastName_5 = "Test empl name 5";

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

        private const string Employee_PersonalPhoneNumber_1 = "09999999991";
        private const string Employee_PersonalPhoneNumber_2 = "09999999992";
        private const string Employee_PersonalPhoneNumber_3 = "09999999993";
        private const string Employee_PersonalPhoneNumber_4 = "09999999994";
        private const string Employee_PersonalPhoneNumber_5 = "09999999995";

        private const string Employee_Position_1 = "Test position 1";
        private const string Employee_Position_2 = "Test position 2";
        private const string Employee_Position_3 = "Test position 3";
        private const string Employee_Position_4 = "Test position 4";
        private const string Employee_Position_5 = "Test position 5";

        private const bool Employee_IsActive_YES = true;
        private const bool Employee_IsActive_NO = false;

        private const int AccessLevel_0 = 0;
        private const int AccessLevel_3 = 3;
        private const int AccessLevel_5 = 5;
        private const int AccessLevel_7 = 7;
        private const int AccessLevel_10 = 10;
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

        #region LeavingReasonsDummyData
        private const string LeavingReason_Retired_Reason = "Retired";
        private const int LeavingReason_Retired_Id = 1;

        private const string LeavingReason_Fired_Reason = "Fired";
        private const int LeavingReason_Fired_Id = 2;

        private const string LeavingReason_Resigned_Reason = "Resigned";
        private const int LeavingReason_Resigned_Id = 3;
        #endregion

        private const string Invalid_String_Id = "Invalid id";
        private const int Invalid_Int_Id = 18;

        private IEmployeesService employeesService;

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

        private List<LeavingReason> GetLeavingReasonsDummyData()
        {
            return new List<LeavingReason>
            {
                new LeavingReason{ Id = LeavingReason_Retired_Id, Reason = LeavingReason_Retired_Reason},
                new LeavingReason{ Id = LeavingReason_Fired_Id, Reason = LeavingReason_Fired_Reason},
                new LeavingReason{ Id = LeavingReason_Resigned_Id, Reason = LeavingReason_Resigned_Reason},
            };
        }

        private List<Employee> GetEmployeesDummyData()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = Employee_Id_1,
                    FirstName = Employee_FirstName_1,
                    MiddleName = Employee_MiddleName_1,
                    LastName = Employee_LastName_1,
                    FullName = Employee_FullName_1,
                    Email = Employee_Email_1,
                    UserName = Employee_Email_1,
                    ProfilePicture = Employee_ProfilePicture_1,
                    PhoneNumber = Employee_PhoneNumber_1,
                    PersonalPhoneNumber = Employee_PersonalPhoneNumber_1,
                    AccessLevel = AccessLevel_10,
                    DateOfBirth = DateTime.UtcNow.AddDays(-10220),
                    AppointedOn = DateTime.UtcNow.AddDays(-20),
                    IsActive = Employee_IsActive_YES,
                    Position = Employee_Position_1,
                    DepartmentId = Department_Id_1
                },
                new Employee
                {
                    Id = Employee_Id_2,
                    FirstName = Employee_FirstName_2,
                    MiddleName = Employee_MiddleName_2,
                    LastName = Employee_LastName_2,
                    FullName = Employee_FullName_2,
                    Email = Employee_Email_2,
                    UserName = Employee_Email_2,
                    ProfilePicture = Employee_ProfilePicture_2,
                    PhoneNumber = Employee_PhoneNumber_2,
                    PersonalPhoneNumber = Employee_PersonalPhoneNumber_2,
                    AccessLevel = AccessLevel_7,
                    DateOfBirth = DateTime.UtcNow.AddDays(-10225),
                    AppointedOn = DateTime.UtcNow.AddDays(-30),
                    IsActive = Employee_IsActive_YES,
                    Position = Employee_Position_2,
                    DepartmentId = Department_Id_1
                },
                new Employee
                {
                    Id = Employee_Id_3,
                    FirstName = Employee_FirstName_3,
                    MiddleName = Employee_MiddleName_3,
                    LastName = Employee_LastName_3,
                    FullName = Employee_FullName_3,
                    Email = Employee_Email_3,
                    UserName = Employee_Email_3,
                    ProfilePicture = Employee_ProfilePicture_3,
                    PhoneNumber = Employee_PhoneNumber_3,
                    PersonalPhoneNumber = Employee_PersonalPhoneNumber_3,
                    AccessLevel = AccessLevel_5,
                    DateOfBirth = DateTime.UtcNow.AddDays(-10221),
                    AppointedOn = DateTime.UtcNow.AddDays(-35),
                    IsActive = Employee_IsActive_YES,
                    Position = Employee_Position_3,
                    DepartmentId = Department_Id_2
                },
                new Employee
                {
                    Id = Employee_Id_4,
                    FirstName = Employee_FirstName_4,
                    MiddleName = Employee_MiddleName_4,
                    LastName = Employee_LastName_4,
                    FullName = Employee_FullName_4,
                    Email = Employee_Email_4,
                    UserName = Employee_Email_4,
                    ProfilePicture = Employee_ProfilePicture_4,
                    AccessLevel = AccessLevel_0,
                    DateOfBirth = DateTime.UtcNow.AddDays(-10229),
                    AppointedOn = DateTime.UtcNow.AddDays(-40),
                    IsActive = Employee_IsActive_NO,
                    LeavingReasonId = LeavingReason_Resigned_Id,
                    LeftOn = DateTime.UtcNow.AddDays(-2),
                    Position = Employee_Position_4,
                    DepartmentId = Department_Id_4
                },
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetLeavingReasonsDummyData());
            await context.AddRangeAsync(GetDepartmentsDummyData());
            await context.SaveChangesAsync();

            await context.AddRangeAsync(GetEmployeesDummyData());
            await context.SaveChangesAsync();
        }

        public EmployeesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task IsEmailValid_WithValidEmail_ShouldReturnTrue()
        {
            string errorMessagePrefix = "EmployeesService IsEmailValid() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var result = this.employeesService.IsEmailValid(Employee_Email_1);

            Assert.True(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmailValid_WithInvalidEmailFromInactiveEmployee_ShouldReturnFalse()
        {
            string errorMessagePrefix = "EmployeesService IsEmailValid() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var result = this.employeesService.IsEmailValid(Employee_Email_4);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmailValid_WithInvalidEmailNotFromTheCompany_ShouldReturnFalse()
        {
            string errorMessagePrefix = "EmployeesService IsEmailValid() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var result = this.employeesService.IsEmailValid(Employee_Email_5);

            Assert.False(result, errorMessagePrefix);
        }

        [Theory]
        [InlineData(Employee_Id_1, Employee_FullName_1)]
        [InlineData(Employee_Id_2, Employee_FullName_2)]
        [InlineData(Employee_Id_3, Employee_FullName_3)]
        [InlineData(Employee_Id_4, Employee_FullName_4)]
        public async Task GetEmployeeFullNameByIdAsync_WithValidEmployeeId_ShouldReturnCorrectResult(string employeeId, string expectedResult)
        {
            string errorMessagePrefix = "EmployeesService GetEmployeeFullNameByIdAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var actualResult = await this.employeesService.GetEmployeeFullNameByIdAsync(employeeId);

            Assert.True(expectedResult == actualResult, errorMessagePrefix + " " + "FullName is not returned correctly!");
        }

        [Fact]
        public async Task GetEmployeeFullNameByIdAsync_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesService.GetEmployeeFullNameByIdAsync(Invalid_String_Id));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, Invalid_String_Id), ex.Message);
        }

        [Fact]
        public async Task CheckIfEmployeeIsInRole_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesService.CheckIfEmployeeIsInRole(Invalid_String_Id, "Admin"));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, Invalid_String_Id), ex.Message);
        }

        [Fact]
        public async Task CheckIfEmployeeIsInRole_WithInvalidRoleName_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            string invalidRoleName = "Invalid Role";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesService.CheckIfEmployeeIsInRole(Employee_Id_1, invalidRoleName));

            Assert.Equal(string.Format(ErrorMessages.RoleNameNullReferenceException, invalidRoleName), ex.Message);
        }

        [Fact]
        public async Task RetrievePasswordAsync_WithInvalidEmail_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();

            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesService.RetrievePasswordAsync(Employee_Email_5));

            Assert.Equal(string.Format(ErrorMessages.EmployeeEmailNullReference, Employee_Email_5), ex.Message);
        }

        [Fact]
        public async Task RetrievePasswordAsync_WithValidEmailAndFailedSendGrid_ShouldThrowArgumentException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            emailSender.Setup(es => es.SendRegistrationMailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => this.employeesService.RetrievePasswordAsync(Employee_Email_1));

            Assert.Equal(ErrorMessages.SendGridMailArgumentException, ex.Message);
        }

        [Fact]
        public async Task ChangePasswordAsync_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            EmployeeChangePasswordDto input = new EmployeeChangePasswordDto { CurrentPassword = "RandomPassword 1", NewPassword = "RandomPassword 2" };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesService.ChangePasswordAsync(Employee_Id_5, input));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, Employee_Id_5), ex.Message);
        }

        [Fact]
        public async Task ValidateCurrentPasswordAsync_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            string currentPassword = "RandomPassword 1";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesService.ValidateCurrentPasswordAsync(Employee_Id_5, currentPassword));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, Employee_Id_5), ex.Message);
        }

        [Fact]
        public async Task GetEmployeeDtoByUsernameAsync_WithInvalidUserName_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var ex = Assert.Throws<NullReferenceException>(() => this.employeesService.GetEmployeeDtoByUsernameAsync<ActiveEmployeeDepartmentDto>(Employee_Email_5));

            Assert.Equal(string.Format(ErrorMessages.EmployeeUsernameNullReference, Employee_Email_5), ex.Message);
        }

        [Fact]
        public async Task GetEmployeeDtoByUsernameAsync_WithValidUserName_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "EmployeesService GetEmployeeDtoByUsernameAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            ActiveEmployeeDepartmentDto expectedResult = (await context.Users.SingleAsync(e => e.UserName == Employee_Email_1)).To<ActiveEmployeeDepartmentDto>();

            ActiveEmployeeDepartmentDto actualResult = await this.employeesService.GetEmployeeDtoByUsernameAsync<ActiveEmployeeDepartmentDto>(Employee_Email_1).SingleAsync();

            Assert.True(expectedResult.FullName == actualResult.FullName, errorMessagePrefix + " " + "FullName not returned correctly!");
            Assert.True(expectedResult.DepartmentName == actualResult.DepartmentName, errorMessagePrefix + " " + "DepartmentName not returned correctly!");
        }

        [Fact]
        public async Task GetEmployeeDtoByIdAsync_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var ex = Assert.Throws<NullReferenceException>(() => this.employeesService.GetEmployeeDtoByIdAsync<EmployeeEditDto>(Employee_Id_5));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, Employee_Id_5), ex.Message);
        }

        [Fact]
        public async Task GetEmployeeDtoByIdAsync_WithValidEmployeeId_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "EmployeesService GetEmployeeDtoByIdAsyncc() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            EmployeeEditDto expectedResult = (await context.Users.SingleAsync(e => e.Id == Employee_Id_1)).To<EmployeeEditDto>();

            EmployeeEditDto actualResult = await this.employeesService.GetEmployeeDtoByIdAsync<EmployeeEditDto>(Employee_Id_1).SingleAsync();

            Assert.True(expectedResult.FullName == actualResult.FullName, errorMessagePrefix + " " + "FullName not returned correctly!");
            Assert.True(expectedResult.AccessLevel == actualResult.AccessLevel, errorMessagePrefix + " " + "AccessLevel not returned correctly!");
            Assert.True(expectedResult.AppointedOn == actualResult.AppointedOn, errorMessagePrefix + " " + "AppointedOn not returned correctly!");
            Assert.True(expectedResult.DateOfBirth == actualResult.DateOfBirth, errorMessagePrefix + " " + "DateOfBirth not returned correctly!");
            Assert.True(expectedResult.DepartmentId == actualResult.DepartmentId, errorMessagePrefix + " " + "DepartmentId not returned correctly!");
            Assert.True(expectedResult.Email == actualResult.Email, errorMessagePrefix + " " + "Email not returned correctly!");
            Assert.True(expectedResult.FirstName == actualResult.FirstName, errorMessagePrefix + " " + "FirstName not returned correctly!");
            Assert.True(expectedResult.LastName == actualResult.LastName, errorMessagePrefix + " " + "LastName not returned correctly!");
            Assert.True(expectedResult.MiddleName == actualResult.MiddleName, errorMessagePrefix + " " + "MiddleName not returned correctly!");
            Assert.True(expectedResult.Position == actualResult.Position, errorMessagePrefix + " " + "Position not returned correctly!");
            Assert.True(expectedResult.ProfilePicture == actualResult.ProfilePicture, errorMessagePrefix + " " + "ProfilePicture not returned correctly!");
            Assert.True(expectedResult.PhoneNumber == actualResult.PhoneNumber, errorMessagePrefix + " " + "PhoneNumber not returned correctly!");
            Assert.True(expectedResult.PersonalPhoneNumber == actualResult.PersonalPhoneNumber, errorMessagePrefix + " " + "PersonalPhoneNumber not returned correctly!");
        }

        [Fact]
        public async Task GetAllInactiveEmployees_WithValidData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "EmployeesService GetAllInactiveEmployees() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            List<EmployeeInactiveDto> expectedResult = await context.Users.Where(e => e.IsActive == Employee_IsActive_NO).To<EmployeeInactiveDto>().ToListAsync();

            List<EmployeeInactiveDto> actualResult = await this.employeesService.GetAllInactiveEmployees().ToListAsync();

            Assert.True(expectedResult.Count == actualResult.Count, errorMessagePrefix + " " + "Employees not returned correctly!");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                var expectedEntry = expectedResult[i];
                var actualEntry = actualResult[i];

                Assert.True(expectedEntry.DepartmentName == actualEntry.DepartmentName, errorMessagePrefix + " " + "DepartmentName not returned correctly!");
                Assert.True(expectedEntry.Email == actualEntry.Email, errorMessagePrefix + " " + "Email not returned correctly!");
                Assert.True(expectedEntry.FullName == actualEntry.FullName, errorMessagePrefix + " " + "FullName not returned correctly!");
                Assert.True(expectedEntry.LeavingReasonReason == actualEntry.LeavingReasonReason, errorMessagePrefix + " " + "LeavingReasonReason not returned correctly!");
                Assert.True(expectedEntry.LeftOn == actualEntry.LeftOn, errorMessagePrefix + " " + "LeftOn not returned correctly!");
                Assert.True(expectedEntry.Position == actualEntry.Position, errorMessagePrefix + " " + "Position not returned correctly!");
                Assert.True(expectedEntry.ProfilePicture == actualEntry.ProfilePicture, errorMessagePrefix + " " + "ProfilePicture not returned correctly!");
            }
        }

        [Fact]
        public async Task GetAllInactiveEmployees_WithZeroData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "EmployeesService GetAllInactiveEmployees() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            List<EmployeeInactiveDto> actualResult = await this.employeesService.GetAllInactiveEmployees().ToListAsync();

            Assert.True(actualResult.Count == 0, errorMessagePrefix + " " + "Employees not returned correctly!");
        }

        [Fact]
        public async Task GetAllActiveEmployees_WithValidData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "EmployeesService GetAllActiveEmployees() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            List<EmployeeActiveDto> expectedResult = await context.Users.Where(e => e.IsActive == Employee_IsActive_YES).To<EmployeeActiveDto>().ToListAsync();

            List<EmployeeActiveDto> actualResult = await this.employeesService.GetAllActiveEmployees().ToListAsync();

            Assert.True(expectedResult.Count == actualResult.Count, errorMessagePrefix + " " + "Employees not returned correctly!");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                var expectedEntry = expectedResult[i];
                var actualEntry = actualResult[i];

                Assert.True(expectedEntry.DepartmentName == actualEntry.DepartmentName, errorMessagePrefix + " " + "DepartmentName not returned correctly!");
                Assert.True(expectedEntry.Email == actualEntry.Email, errorMessagePrefix + " " + "Email not returned correctly!");
                Assert.True(expectedEntry.FullName == actualEntry.FullName, errorMessagePrefix + " " + "FullName not returned correctly!");
                Assert.True(expectedEntry.Position == actualEntry.Position, errorMessagePrefix + " " + "Position not returned correctly!");
                Assert.True(expectedEntry.ProfilePicture == actualEntry.ProfilePicture, errorMessagePrefix + " " + "ProfilePicture not returned correctly!");
                Assert.True(expectedEntry.AppointedOn == actualEntry.AppointedOn, errorMessagePrefix + " " + "AppointedOn not returned correctly!");
                Assert.True(expectedEntry.AccessLevel == actualEntry.AccessLevel, errorMessagePrefix + " " + "AccessLevel not returned correctly!");
            }
        }

        [Fact]
        public async Task GetAllActiveEmployees_WithZeroData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "EmployeesService GetAllActiveEmployees() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            List<EmployeeActiveDto> actualResult = await this.employeesService.GetAllActiveEmployees().ToListAsync();

            Assert.True(actualResult.Count == 0, errorMessagePrefix + " " + "Employees not returned correctly!");
        }

        [Fact]
        public async Task GetActiveEmployeesWithDepartment_WithValidData_ShouldReturnCorrectResult()
        {
            string errorMessagePrefix = "EmployeesService GetActiveEmployeesWithDepartment() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            List<ActiveEmployeeDepartmentDto> expectedResult = await context.Users.Where(e => e.IsActive == Employee_IsActive_YES).To<ActiveEmployeeDepartmentDto>().ToListAsync();

            List<ActiveEmployeeDepartmentDto> actualResult = await this.employeesService.GetActiveEmployeesWithDepartment().ToListAsync();

            Assert.True(expectedResult.Count == actualResult.Count, errorMessagePrefix + " " + "Employees not returned correctly!");
            for (int i = 0; i < expectedResult.Count; i++)
            {
                var expectedEntry = expectedResult[i];
                var actualEntry = actualResult[i];

                Assert.True(expectedEntry.DepartmentName == actualEntry.DepartmentName, errorMessagePrefix + " " + "DepartmentName not returned correctly!");
                Assert.True(expectedEntry.FullName == actualEntry.FullName, errorMessagePrefix + " " + "FullName not returned correctly!");
                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id not returned correctly!");
            }
        }

        [Fact]
        public async Task EditAsync_WithValidData_ShouldEditEmployeeAndReturnTrue()
        {
            string errorMessagePrefix = "EmployeesService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            departmentsService.Setup(ds => ds.GetDepartmentNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Department_Name_4);

            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(um => um.GetRolesAsync(It.IsAny<Employee>())).ReturnsAsync(new List<string> { "Employe" });
            userManager.Setup(um => um.GetClaimsAsync(It.IsAny<Employee>())).ReturnsAsync(new List<Claim> { new Claim ( "AccessLevel", "7"  ) });

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            EmployeeEditDto expectedResult = (await context.Users.SingleAsync(e => e.Id == Employee_Id_1)).To<EmployeeEditDto>();

            expectedResult.AccessLevel = AccessLevel_7;
            expectedResult.AppointedOn = "30-09-2019";
            expectedResult.DateOfBirth = "12-12-1982";
            expectedResult.DepartmentId = Department_Id_4;
            expectedResult.Email = Employee_Email_5;
            expectedResult.FirstName = Employee_FirstName_5;
            expectedResult.MiddleName = Employee_MiddleName_5;
            expectedResult.LastName = Employee_LastName_5;
            expectedResult.PersonalPhoneNumber = Employee_PersonalPhoneNumber_5;
            expectedResult.PhoneNumber = Employee_PhoneNumber_5;
            expectedResult.Position = Employee_Position_5;
            expectedResult.Username = Employee_Email_5;

            var result = await this.employeesService.EditAsync(expectedResult);

            EmployeeEditDto actualResult = (await context.Users.SingleAsync(e => e.Id == Employee_Id_1)).To<EmployeeEditDto>();

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedResult.AccessLevel == actualResult.AccessLevel, errorMessagePrefix + " " + "AccessLevel not changed correctly!");
            Assert.True(expectedResult.AppointedOn == actualResult.AppointedOn, errorMessagePrefix + " " + "AppointedOn not changed correctly!");
            Assert.True(expectedResult.DateOfBirth == actualResult.DateOfBirth, errorMessagePrefix + " " + "DateOfBirth not changed correctly!");
            Assert.True(expectedResult.DepartmentId == actualResult.DepartmentId, errorMessagePrefix + " " + "DepartmentId not changed correctly!");
            Assert.True(expectedResult.Email == actualResult.Email, errorMessagePrefix + " " + "Email not changed correctly!");
            Assert.True(expectedResult.FirstName == actualResult.FirstName, errorMessagePrefix + " " + "FirstName not changed correctly!");
            Assert.True(expectedResult.MiddleName == actualResult.MiddleName, errorMessagePrefix + " " + "MiddleName not changed correctly!");
            Assert.True(expectedResult.LastName == actualResult.LastName, errorMessagePrefix + " " + "LastName not changed correctly!");
            Assert.True(expectedResult.PersonalPhoneNumber == actualResult.PersonalPhoneNumber, errorMessagePrefix + " " + "PersonalPhoneNumber not changed correctly!");
            Assert.True(expectedResult.PhoneNumber == actualResult.PhoneNumber, errorMessagePrefix + " " + "PhoneNumber not changed correctly!");
            Assert.True(expectedResult.Position == actualResult.Position, errorMessagePrefix + " " + "Position not changed correctly!");
            Assert.True(expectedResult.Username == actualResult.Username, errorMessagePrefix + " " + "Username not changed correctly!");
        }

        [Fact]
        public async Task EditAsync_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            EmployeeEditDto employeeToEdit = (await context.Users.SingleAsync(e => e.Id == Employee_Id_1)).To<EmployeeEditDto>();

            employeeToEdit.Id = Employee_Id_5;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesService.EditAsync(employeeToEdit));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, Employee_Id_5), ex.Message);
        }

        [Fact]
        public async Task IsEmailValidToChange_WithValidDataSameMail_ShouldReturnTrue()
        {
            string errorMessagePrefix = "EmployeesService IsEmailValidToChange() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var result = await this.employeesService.IsEmailValidToChange(Employee_Email_1, Employee_Id_1);

            Assert.True(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmailValidToChange_WithValidDataUniqueMail_ShouldReturnTrue()
        {
            string errorMessagePrefix = "EmployeesService IsEmailValidToChange() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var result = await this.employeesService.IsEmailValidToChange(Employee_Email_5, Employee_Id_1);

            Assert.True(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmailValidToChange_WithInvalidDataAlreadyRegisterdMail_ShouldReturnFalse()
        {
            string errorMessagePrefix = "EmployeesService IsEmailValidToChange() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var result = await this.employeesService.IsEmailValidToChange(Employee_Email_3, Employee_Id_1);

            Assert.False(result, errorMessagePrefix);
        }

        [Fact]
        public async Task IsEmailValidToChange_WithInvalidEmployeeId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();
            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.employeesService.IsEmailValidToChange(Employee_Email_3, Employee_Id_5));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, Employee_Id_5), ex.Message);
        }

        [Fact]
        public async Task ReleaseAsync_WithValidData_ShouldChangeEmployeeCorrectlyAndReturnTrue()
        {
            string errorMessagePrefix = "EmployeesService ReleaseAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            var departmentsService = new Mock<IDepartmentsService>();

            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(um => um.GetRolesAsync(It.IsAny<Employee>())).ReturnsAsync(new List<string> { "Employe" });
            userManager.Setup(um => um.GetClaimsAsync(It.IsAny<Employee>())).ReturnsAsync(new List<Claim> { new Claim("AccessLevel", "7") });

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            EmployeeReleaseDto expectedResult = (await context.Users.SingleAsync(e => e.Id == Employee_Id_1)).To<EmployeeReleaseDto>();
            expectedResult.LeavingReasonId = LeavingReason_Fired_Id;
            expectedResult.LeftOn = "31-08-2019";

            var result = await this.employeesService.ReleaseAsync(expectedResult);

            var actualResult = await context.Users.SingleAsync(e => e.Id == Employee_Id_1);

            Assert.True(result, errorMessagePrefix);
            Assert.True(expectedResult.LeavingReasonId == actualResult.LeavingReasonId, errorMessagePrefix + " " + "LeavingReasonId is not changed correctly!");
            Assert.True(actualResult.LeftOn.HasValue, errorMessagePrefix + " " + "LeftOn is not changed correctly!");
            Assert.False(actualResult.IsActive, errorMessagePrefix + " " + "IsActive is not changed correctly!");
            Assert.True(actualResult.PhoneNumber == null, errorMessagePrefix + " " + "PhoneNumber is not changed correctly!");
            Assert.True(actualResult.PersonalPhoneNumber == null, errorMessagePrefix + " " + "PersonalPhoneNumber is not changed correctly!");
        }

        [Fact]
        public async Task HireBackAsync_WithValidData_ShouldChangeEmployeeCorrectlyAndReturnTrue()
        {
            string errorMessagePrefix = "EmployeesService HireBackAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            var emailSender = new Mock<ISendGrid>();
            emailSender.Setup(es => es.SendRegistrationMailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            var departmentsService = new Mock<IDepartmentsService>();
            departmentsService.Setup(ds => ds.GetDepartmentNameByIdAsync(It.IsAny<int>())).ReturnsAsync(Department_Name_4);

            var store = new Mock<IUserStore<Employee>>();
            var userManager = new Mock<UserManager<Employee>>(store.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(um => um.AddToRolesAsync(It.IsAny<Employee>(), It.IsAny<IEnumerable<string>>())).ReturnsAsync(new IdentityResult());
            userManager.Setup(um => um.GetRolesAsync(It.IsAny<Employee>())).ReturnsAsync(new List<string> { "Employe" });
            userManager.Setup(um => um.GetClaimsAsync(It.IsAny<Employee>())).ReturnsAsync(new List<Claim> { new Claim("AccessLevel", "7") });

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<Employee>>();
            var signInManager = new Mock<SignInManager<Employee>>(userManager.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            this.employeesService = new EmployeesService(userManager.Object, signInManager.Object, context, departmentsService.Object, emailSender.Object);

            EmployeeHireBackDto expectedResult = (await context.Users.SingleAsync(e => e.Id == Employee_Id_4)).To<EmployeeHireBackDto>();
            expectedResult.PhoneNumber = Employee_PhoneNumber_5;
            expectedResult.PersonalPhoneNumber = Employee_PersonalPhoneNumber_5;
            expectedResult.AccessLevel = AccessLevel_5;
            expectedResult.AppointedOn = "31-08-2019";

            var result = await this.employeesService.HireBackAsync(expectedResult);

            var actualResult = await context.Users.SingleAsync(e => e.Id == Employee_Id_4);

            Assert.True(result, errorMessagePrefix);
            Assert.False(actualResult.LeavingReasonId.HasValue, errorMessagePrefix + " " + "LeavingReasonId is not changed correctly!");
            Assert.False(actualResult.LeftOn.HasValue, errorMessagePrefix + " " + "LeftOn is not changed correctly!");
            Assert.True(actualResult.IsActive, errorMessagePrefix + " " + "IsActive is not changed correctly!");
            Assert.True(expectedResult.PhoneNumber == actualResult.PhoneNumber, errorMessagePrefix + " " + "PhoneNumber is not changed correctly!");
            Assert.True(expectedResult.PersonalPhoneNumber == actualResult.PersonalPhoneNumber, errorMessagePrefix + " " + "PersonalPhoneNumber is not changed correctly!");
            Assert.True(expectedResult.AppointedOn == actualResult.AppointedOn.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture), errorMessagePrefix + " " + "AppointedOn is not changed correctly!");
        }
    }
}
