using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assets;

namespace OMM.App.Models.ViewModels
{
    public class EmployeeProfileAssetsViewModel : IMapFrom<AssetEmployeeDto>
    {
        public string InventoryNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ReferenceNumber { get; set; }

        public string DateOfAquire { get; set; }

        public string AssetTypeName { get; set; }
    }
}
