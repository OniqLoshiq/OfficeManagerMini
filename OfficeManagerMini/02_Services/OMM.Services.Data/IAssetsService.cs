using OMM.Services.Data.DTOs.Assets;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IAssetsService
    {
        Task<bool> CreateAsync(AssetCreateDto assetModel);
    }
}
