using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assets;

namespace OMM.App.Areas.Management.Models.ViewModels
{
    public class AssetEmployeeViewModel : IMapFrom<AssetEmployeeDto>
    {
        public string InventoryNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ReferenceNumber { get; set; }

        public string DateOfAquire { get; set; }

        public string AssetTypeName { get; set; }
    }
}
