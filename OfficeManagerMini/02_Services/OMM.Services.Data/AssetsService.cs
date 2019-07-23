using AutoMapper;
using OMM.Data;
using OMM.Domain;
using OMM.Services.Data.DTOs.Assets;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public class AssetsService : IAssetsService
    {
        private readonly OmmDbContext context;

        public AssetsService(OmmDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateAsync(AssetCreateDto assetModel)
        {
            var asset = Mapper.Map<Asset>(assetModel);

            this.context.Assets.Add(asset);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

    }
}
