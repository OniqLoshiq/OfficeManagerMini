using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Statuses
{
    public class StatusListDto : IMapFrom<Status>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
