using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System.Globalization;

namespace OMM.Services.Data.DTOs.Assets
{
    public class AssetListDto : IMapFrom<Asset>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string InventoryNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ReferenceNumber { get; set; }

        public string DateOfAquire { get; set; }

        public string AssetType { get; set; }

        public string EmployeeFullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Asset, AssetListDto>()
                .ForMember(destination => destination.DateOfAquire,
                    options => options.MapFrom(origin => origin.DateOfAquire.ToString(Constants.DATETIME_FORMAT)))
               .ForMember(destination => destination.AssetType,
                    options => options.MapFrom(origin => origin.AssetType.Name))
               .ForMember(destination => destination.EmployeeFullName,
                    options => options.MapFrom(origin => origin.Employee.FullName));
        }
    }
}
