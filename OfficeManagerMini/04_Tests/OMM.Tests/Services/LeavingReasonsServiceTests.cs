using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.LeavingReasons;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class LeavingReasonsServiceTests
    {
        private const string LeavingReason_Retired_Reason = "Retired";
        private const int LeavingReason_Retired_Id = 1;

        private const string LeavingReason_Fired_Reason = "Fired";
        private const int LeavingReason_Fired_Id = 2;

        private const string LeavingReason_Resigned_Reason = "Resigned";
        private const int LeavingReason_Resigned_Id = 3;


        private ILeavingReasonsService leavingReasonsService;

        private List<LeavingReason> GetDummyData()
        {
            return new List<LeavingReason>
            {
                new LeavingReason{ Id = LeavingReason_Retired_Id, Reason = LeavingReason_Retired_Reason},
                new LeavingReason{ Id = LeavingReason_Fired_Id, Reason = LeavingReason_Fired_Reason},
                new LeavingReason{ Id = LeavingReason_Resigned_Id, Reason = LeavingReason_Resigned_Reason},
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyData());
            await context.SaveChangesAsync();
        }

        public LeavingReasonsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetLeavingReasons_WithDummyData_ShouldReturnCorrectResults()
        {
            string errorMessagePrefix = "LeavingReasons GetLeavingReasons() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.leavingReasonsService = new LeavingReasonsService(context);

            List<LeavingReasonDto> actualResults = await this.leavingReasonsService.GetLeavingReasons().ToListAsync();
            List<LeavingReasonDto> expectedResults = GetDummyData().To<LeavingReasonDto>().ToList();

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Id == actualEntry.Id, errorMessagePrefix + " " + "Id is not returned properly.");
                Assert.True(expectedEntry.Reason == actualEntry.Reason, errorMessagePrefix + " " + "Reason is not returned properly.");
            }
            Assert.True(expectedResults.Count == actualResults.Count, errorMessagePrefix + " " + "Count of returned leaving reasons is not correct");
        }

        [Fact]
        public async Task GetLeavingReasons_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "LeavingReasons GetLeavingReasons() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            this.leavingReasonsService = new LeavingReasonsService(context);

            List<LeavingReasonDto> actualResults = await this.leavingReasonsService.GetLeavingReasons().ToListAsync();

            Assert.True(actualResults.Count == 0, errorMessagePrefix + " " + "Count of returned leaving reasons is not correct");
        }
    }
}
