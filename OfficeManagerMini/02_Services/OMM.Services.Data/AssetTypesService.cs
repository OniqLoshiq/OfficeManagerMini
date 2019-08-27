using OMM.Data;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.AssetTypes;
using System.Linq;

namespace OMM.Services.Data
{
    public class AssetTypesService : IAssetTypesService
    {
        private readonly OmmDbContext context;

        public AssetTypesService(OmmDbContext context)
        {
            this.context = context;
        }


        public IQueryable<AssetTypeSelectListDto> GetAll()
        {
            return this.context.AssetTypes.To<AssetTypeSelectListDto>();
        }
    }
}
