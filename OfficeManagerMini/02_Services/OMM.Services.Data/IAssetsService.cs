using OMM.Services.Data.DTOs.Assets;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IAssetsService
    {
        Task<bool> CreateAsync(AssetCreateDto assetModel);

        IQueryable<AssetListDto> GetAll();

        Task<bool> DeleteAsync(string id);

        Task<AssetEditDto> GetAssetByIdAsync(string id);

        Task<bool> EditAsync(AssetEditDto assetToEdit);

        IQueryable<AssetEmployeeDto> GetAssetsByEmployeeId(string employeeId);
    }
}
