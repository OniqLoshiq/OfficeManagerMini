using OMM.Domain;
using System.Linq;

namespace OMM.Data.Seeding
{
    public class AssetTypeSeeder : ISeeder
    {
        private readonly OmmDbContext context;

        public AssetTypeSeeder(OmmDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if (this.context.AssetTypes.Any())
            {
                return;
            }
            
            var assetTypes = new AssetType[]
            {
                new AssetType{ Name = "Software"},
                new AssetType{ Name = "Vehicle"},
                new AssetType{ Name = "Transport"},
                new AssetType{ Name = "Communication"},
                new AssetType{ Name = "Social"},
                new AssetType{ Name = "Office Equipment"},
            };

            foreach (var assetType in assetTypes)
            {
                this.context.AssetTypes.Add(assetType);
            }

            this.context.SaveChanges();
        }
    }
}
