using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.AssetTypes
{
    public class AssetTypeSelectListDto : IMapFrom<AssetType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
