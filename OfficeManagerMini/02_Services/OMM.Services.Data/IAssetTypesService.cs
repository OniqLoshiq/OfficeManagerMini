using OMM.Services.Data.DTOs.AssetTypes;
using System.Linq;

namespace OMM.Services.Data
{
    public interface IAssetTypesService
    {
        IQueryable<AssetTypeSelectListDto> GetAll();
    }
}
