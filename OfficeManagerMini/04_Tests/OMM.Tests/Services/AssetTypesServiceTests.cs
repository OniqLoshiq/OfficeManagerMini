using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.AssetTypes;
using OMM.Tests.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class AssetTypesServiceTests
    {
        private const string AssetType_Vehicle_Name = "Vehicle";
        private const int AssetType_Vehicle_Id = 1;

        private const string AssetType_Transport_Name = "Transport";
        private const int AssetType_Transport_Id = 2;

        private const string AssetType_Communication_Name = "Communication";
        private const int AssetType_Communication_Id = 3;

        private const string AssetType_Social_Name = "Social";
        private const int AssetType_Social_Id = 4;

        private const string AssetType_OfficeEquipment_Name = "Office Equipment";
        private const int AssetType_OfficeEquipment_Id = 5;


        private IAssetTypesService assetTypesService;

        private List<AssetType> GetDummyData()
        {
            return new List<AssetType>
            {
                new AssetType{ Id = AssetType_Vehicle_Id, Name = AssetType_Vehicle_Name},
                new AssetType{ Id = AssetType_Transport_Id, Name = AssetType_Transport_Name},
                new AssetType{ Id = AssetType_Communication_Id, Name = AssetType_Communication_Name},
                new AssetType{ Id = AssetType_Social_Id, Name = AssetType_Social_Name},
                new AssetType{ Id = AssetType_OfficeEquipment_Id, Name = AssetType_OfficeEquipment_Name},
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyData());
            await context.SaveChangesAsync();
        }

        public AssetTypesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetAll_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "AssetTypes GetAll() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.assetTypesService = new AssetTypesService(context);

            List<AssetTypeSelectListDto> actualResults = await this.assetTypesService.GetAll().ToListAsync();
            List<AssetTypeSelectListDto> expectedResults = GetDummyData().To<AssetTypeSelectListDto>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Name == actualEntry.Name, errorMessagePrefix + " " + "Name is not returned properly.");
            }
            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returned asset types is not correct");
        }

        [Fact]
        public async Task GetAll_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "AssetTypes GetAll() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            this.assetTypesService = new AssetTypesService(context);

            List<AssetTypeSelectListDto> actualResults = await this.assetTypesService.GetAll().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returned asset types is not correct");
        }
    }
}
