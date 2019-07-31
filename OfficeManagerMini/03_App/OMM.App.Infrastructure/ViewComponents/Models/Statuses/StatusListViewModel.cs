using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Statuses;

namespace OMM.App.Infrastructure.ViewComponents.Models.Statuses
{
    public class StatusListViewModel : IMapFrom<StatusListDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
