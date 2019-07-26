using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;

namespace OMM.Services.Data.DTOs.Assets
{
    public class AssetEmployeeDto : IMapFrom<Asset>, IHaveCustomMappings
    {
        public string InventoryNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ReferenceNumber { get; set; }

        public string DateOfAquire { get; set; }

        public string AssetTypeName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Asset, AssetEmployeeDto>()
                .ForMember(dest => dest.DateOfAquire,
                opts => opts.MapFrom(origin => origin.DateOfAquire.ToString(Constants.DATETIME_FORMAT)));
        }
    }
}
