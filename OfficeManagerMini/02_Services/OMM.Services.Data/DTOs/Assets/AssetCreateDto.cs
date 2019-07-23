using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System;
using System.Globalization;

namespace OMM.Services.Data.DTOs.Assets
{
    public class AssetCreateDto : IMapTo<Asset>, IHaveCustomMappings
    {
        public string InventoryNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ReferenceNumber { get; set; }

        public string DateOfAquire { get; set; }

        public int AssetTypeId { get; set; }

        public string EmployeeId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<AssetCreateDto, Asset>()
                .ForMember(destination => destination.DateOfAquire,
                            opts => opts.MapFrom(origin => DateTime.ParseExact(origin.DateOfAquire, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture)));
        }
    }
}
