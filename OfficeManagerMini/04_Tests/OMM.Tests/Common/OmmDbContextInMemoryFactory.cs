using Microsoft.EntityFrameworkCore;
using OMM.Data;
using System;

namespace OMM.Tests.Common
{
    public static class OmmDbContextInMemoryFactory
    {
        public static OmmDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<OmmDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new OmmDbContext(options);
        }
    }
}
