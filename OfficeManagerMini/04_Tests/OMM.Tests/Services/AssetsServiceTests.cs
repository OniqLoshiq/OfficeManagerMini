using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Assets;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class AssetsServiceTests
    {
        private const string InventoryNumber1 = "190828-OLC-Asset1";
        private const string InventoryNumber2 = "190828-OLC-Asset2";
        private const string InventoryNumber3 = "190828-OLC-Asset3";
        private const string InventoryNumber4 = "190828-OLC-Asset4";

        private const string Make1 = "Microsoft";
        private const string Make2 = "Apple";
        private const string Make3 = "Cannon";
        private const string Make4 = "Audi";

        private const string Model1 = "Office 2016";
        private const string Model2 = "iPhone X";
        private const string Model3 = "iAR 5000";
        private const string Model4 = "A4";

        private const string ReferenceNumber1 = "OLC-01-05-SF";
        private const string ReferenceNumber2 = "OLC-01-02-SC";
        private const string ReferenceNumber3 = "OLC-01-01-PP";
        private const string ReferenceNumber4 = "OLC-01-05-VH";

        private const string Employee_Id1 = "01";
        private const string Employee_FullName1 = "Test Employee 1";

        private const string Employee_Id2 = "02";
        private const string Employee_FullName2 = "Test Employee 2";

        private const int AssetType_Id1 = 1;
        private const string AssetType_Name1 = "Software";

        private const int AssetType_Id2 = 2;
        private const string AssetType_Name2 = "Social";

        private const int AssetType_Id3 = 3;
        private const string AssetType_Name3 = "Office Equipment";

        private const int AssetType_Id4 = 4;
        private const string AssetType_Name4 = "Vehicle";




        private IAssetsService assetsService;

        private List<Asset> GetDummyData()
        {
            return new List<Asset>
            {
                new Asset
                {
                    InventoryNumber = InventoryNumber1,
                    Make = Make1,
                    Model = Model1,
                    ReferenceNumber = ReferenceNumber1,
                    DateOfAquire = DateTime.UtcNow.AddDays(-15),
                    AssetType = new AssetType {Id = AssetType_Id1, Name = AssetType_Name1 },
                    Employee = new Employee {Id = Employee_Id1, FullName = Employee_FullName1 },
                },
                new Asset
                {
                    InventoryNumber = InventoryNumber2,
                    Make = Make2,
                    Model = Model2,
                    ReferenceNumber = ReferenceNumber2,
                    DateOfAquire = DateTime.UtcNow.AddDays(-5),
                    AssetType = new AssetType {Id = AssetType_Id2, Name = AssetType_Name2 },
                    Employee = new Employee {Id = Employee_Id2, FullName = Employee_FullName2 },
                },
                new Asset
                {
                    InventoryNumber = InventoryNumber3,
                    Make = Make3,
                    Model = Model3,
                    ReferenceNumber = ReferenceNumber3,
                    DateOfAquire = DateTime.UtcNow,
                    AssetType = new AssetType {Id = AssetType_Id3, Name = AssetType_Name3 },
                },
                new Asset
                {
                    InventoryNumber = InventoryNumber4,
                    Make = Make4,
                    Model = Model4,
                    ReferenceNumber = ReferenceNumber4,
                    DateOfAquire = DateTime.UtcNow.AddDays(-1),
                    AssetType = new AssetType {Id = AssetType_Id4, Name = AssetType_Name4 },
                }
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyData());
            await context.SaveChangesAsync();
        }

        public AssetsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetAll_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "AssetsService GetAll() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            List<AssetListDto> actualResults = await this.assetsService.GetAll().ToListAsync();
            List<AssetListDto> expectedResults = GetDummyData().To<AssetListDto>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.InventoryNumber == actualEntry.InventoryNumber, errorMessagePrefix + " " + "InventoryNumber is not returned properly.");
                Assert.True(expectedEntry.Make == actualEntry.Make, errorMessagePrefix + " " + "Make is not returned properly.");
                Assert.True(expectedEntry.Model == actualEntry.Model, errorMessagePrefix + " " + "Model is not returned properly.");
                Assert.True(expectedEntry.ReferenceNumber == actualEntry.ReferenceNumber, errorMessagePrefix + " " + "ReferenceNumber is not returned properly.");
                Assert.True(expectedEntry.AssetType == actualEntry.AssetType, errorMessagePrefix + " " + "AssetType name is not returned properly.");
                Assert.True(expectedEntry.EmployeeFullName == actualEntry.EmployeeFullName, errorMessagePrefix + " " + "EmployeeFullName is not returned properly.");
                Assert.True(expectedEntry.DateOfAquire == actualEntry.DateOfAquire, errorMessagePrefix + " " + "DateOfAquire is not returned properly.");
            }
            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returned assets is not correct");
        }

        [Fact]
        public async Task GetAll_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "AssetsService GetAll() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            this.assetsService = new AssetsService(context);

            List<AssetListDto> actualResults = await this.assetsService.GetAll().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returned assets is not correct");
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ShouldCreateAssetAndReturnTrue()
        {
            string errorMessagePrefix = "AssetsService CreateAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            var assetToCreate = new AssetCreateDto
            {
                InventoryNumber = "190828-OLC-Asset5",
                Make = "Microsoft",
                Model = "Windows 10 Professional",
                ReferenceNumber = "OLC-01-09-SC",
                DateOfAquire = "27-08-2019",
                AssetTypeId = 1,
                EmployeeId = "02",
            };

            bool actualResult = await this.assetsService.CreateAsync(assetToCreate);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ShouldDeleteAssetAndReturnTrue()
        {
            string errorMessagePrefix = "AssetsService DeleteAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            var assetToDeleteId = (await context.Assets.FirstAsync()).Id;

            bool actualResult = await this.assetsService.DeleteAsync(assetToDeleteId);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            string invalidId = "Invalid Id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assetsService.DeleteAsync(invalidId));

            Assert.Equal(string.Format(ErrorMessages.AssetIdNullReference, invalidId), ex.Message);
        }

        [Theory]
        [InlineData(InventoryNumber1)]
        [InlineData(InventoryNumber2)]
        [InlineData(InventoryNumber3)]
        [InlineData(InventoryNumber4)]
        public async Task GetAssetByIdAsync_WithValidId_ShouldReturnCorrectResult(string inventoryNumber)
        {
            string errorMessagePrefix = "AssetsService GetAssetByIdAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            string assetId = (await context.Assets.FirstAsync(a => a.InventoryNumber == inventoryNumber)).Id;

            var actualResult = await this.assetsService.GetAssetByIdAsync(assetId);
            var expectedResult = GetDummyData().Single(a => a.InventoryNumber == inventoryNumber).To<AssetEditDto>();

            Assert.True(expectedResult.InventoryNumber == actualResult.InventoryNumber, errorMessagePrefix + " " + "InventoryNumber is not returned properly.");
            Assert.True(expectedResult.Make == actualResult.Make, errorMessagePrefix + " " + "Make is not returned properly.");
            Assert.True(expectedResult.Model == actualResult.Model, errorMessagePrefix + " " + "Model is not returned properly.");
            Assert.True(expectedResult.ReferenceNumber == actualResult.ReferenceNumber, errorMessagePrefix + " " + "ReferenceNumber is not returned properly.");
            Assert.True(expectedResult.DateOfAquire == actualResult.DateOfAquire, errorMessagePrefix + " " + "DateOfAquire is not returned properly.");
        }

        [Fact]
        public async Task GetAssetByIdAsync_WithInvalidId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            string invalidId = "Invalid Id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assetsService.GetAssetByIdAsync(invalidId));

            Assert.Equal(string.Format(ErrorMessages.AssetIdNullReference, invalidId), ex.Message);
        }

        [Theory]
        [InlineData(Employee_Id1)]
        [InlineData(Employee_Id2)]
        public async Task GetAssetsByEmployeeId_WithEmployeeWithAssets_ShouldReturnCorrectResult(string employeeId)
        {
            string errorMessagePrefix = "AssetsService GetAssetsByEmployeeId() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            List<AssetEmployeeDto> actualResults = await this.assetsService.GetAssetsByEmployeeId(employeeId).ToListAsync();
            List<AssetEmployeeDto> expectedResults = GetDummyData().Where(a => a.Employee != null && a.Employee.Id == employeeId).To<AssetEmployeeDto>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.InventoryNumber == actualEntry.InventoryNumber, errorMessagePrefix + " " + "InventoryNumber is not returned properly.");
                Assert.True(expectedEntry.Make == actualEntry.Make, errorMessagePrefix + " " + "Make is not returned properly.");
                Assert.True(expectedEntry.Model == actualEntry.Model, errorMessagePrefix + " " + "Model is not returned properly.");
                Assert.True(expectedEntry.DateOfAquire == actualEntry.DateOfAquire, errorMessagePrefix + " " + "DateOfAquire is not returned properly.");
                Assert.True(expectedEntry.ReferenceNumber == actualEntry.ReferenceNumber, errorMessagePrefix + " " + "ReferenceNumber is not returned properly.");
                Assert.True(expectedEntry.AssetTypeName == actualEntry.AssetTypeName, errorMessagePrefix + " " + "AssetTypeName is not returned properly.");

            }
            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returned assets is not correct");
        }

        [Fact]
        public async Task GetAssetsByEmployeeId_WithEmployeeWithoutAssets_ShouldReturnEmptyColleciton()
        {
            string errorMessagePrefix = "AssetsService GetAssetsByEmployeeId() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            List<AssetEmployeeDto> actualResults = await this.assetsService.GetAssetsByEmployeeId("Invalid").ToListAsync();
            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returned assets is not correct");
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldEditAssetCorrectly()
        {
            string errorMessagePrefix = "AssetsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            AssetEditDto expectedResult = (await context.Assets.FirstAsync()).To<AssetEditDto>();

            expectedResult.ReferenceNumber = "New Ref Number";
            expectedResult.Make = "New make";
            expectedResult.Model = "New model";
            expectedResult.InventoryNumber = "190828-OLC-Changed Number";
            expectedResult.DateOfAquire = "29-08-2019";
            expectedResult.EmployeeId = "02";
            expectedResult.AssetTypeId = 3;

            await this.assetsService.EditAsync(expectedResult);

            var actualResult = (await context.Assets.FirstAsync()).To<AssetEditDto>();

            Assert.True(expectedResult.InventoryNumber == actualResult.InventoryNumber, errorMessagePrefix + " " + "InventoryNumber is not changed properly.");
            Assert.True(expectedResult.Make == actualResult.Make, errorMessagePrefix + " " + "Make is not changed properly.");
            Assert.True(expectedResult.Model == actualResult.Model, errorMessagePrefix + " " + "Model is not changed properly.");
            Assert.True(expectedResult.ReferenceNumber == actualResult.ReferenceNumber, errorMessagePrefix + " " + "ReferenceNumber is not changed properly.");
            Assert.True(expectedResult.DateOfAquire == actualResult.DateOfAquire, errorMessagePrefix + " " + "DateOfAquire is not changed properly.");
            Assert.True(expectedResult.EmployeeId == actualResult.EmployeeId, errorMessagePrefix + " " + "EmployeeId is not changed properly.");
            Assert.True(expectedResult.AssetTypeId == actualResult.AssetTypeId, errorMessagePrefix + " " + "AssetTypeId is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_WithInvalidAssetType_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            int invalidAssetTypeId = 22;

            AssetEditDto expectedResult = (await context.Assets.FirstAsync()).To<AssetEditDto>();

            expectedResult.ReferenceNumber = "New Ref Number";
            expectedResult.Make = "New make";
            expectedResult.Model = "New model";
            expectedResult.InventoryNumber = "190828-OLC-Changed Number";
            expectedResult.DateOfAquire = "29-08-2019";
            expectedResult.EmployeeId = "02";
            expectedResult.AssetTypeId = invalidAssetTypeId;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assetsService.EditAsync(expectedResult));

            Assert.Equal(string.Format(ErrorMessages.AssetTypeIdNullReference, expectedResult.AssetTypeId), ex.Message);
        }

        [Fact]
        public async Task EditAsync_WithInvalidAssetId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetsService = new AssetsService(context);

            string invalidAssetId = "InvalidId";

            AssetEditDto expectedResult = (await context.Assets.FirstAsync()).To<AssetEditDto>();

            expectedResult.ReferenceNumber = "New Ref Number";
            expectedResult.Make = "New make";
            expectedResult.Model = "New model";
            expectedResult.InventoryNumber = "190828-OLC-Changed Number";
            expectedResult.DateOfAquire = "29-08-2019";
            expectedResult.EmployeeId = "02";
            expectedResult.AssetTypeId = 3;
            expectedResult.Id = invalidAssetId;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.assetsService.EditAsync(expectedResult));

            Assert.Equal(string.Format(ErrorMessages.AssetIdNullReference, expectedResult.Id), ex.Message);
        }
    }
}
