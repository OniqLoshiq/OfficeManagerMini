using System.Linq;
using OMM.Data;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.LeavingReasons;

namespace OMM.Services.Data
{
    public class LeavingReasonsService : ILeavingReasonsService
    {
        private readonly OmmDbContext context;

        public LeavingReasonsService(OmmDbContext context)
        {
            this.context = context;
        }


        public IQueryable<LeavingReasonDto> GetLeavingReasons()
        {
            return this.context.LeavingReasons.To<LeavingReasonDto>();
        }
    }
}
