using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.AssetTypes;

namespace OMM.App.Infrastructure.ViewComponents.Models.AssetTypes
{
    public class AssetTypeSelectItemViewModel : IMapFrom<AssetTypeSelectListDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
