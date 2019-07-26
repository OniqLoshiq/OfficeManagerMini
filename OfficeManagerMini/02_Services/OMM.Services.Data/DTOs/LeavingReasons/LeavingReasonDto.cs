using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.LeavingReasons
{
    public class LeavingReasonDto : IMapFrom<LeavingReason>
    {
        public int Id { get; set; }

        public string Reason { get; set; }
    }
}
