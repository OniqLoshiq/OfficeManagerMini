using OMM.Services.Data.DTOs.LeavingReasons;
using System.Linq;

namespace OMM.Services.Data
{
    public interface ILeavingReasonsService
    {
        IQueryable<LeavingReasonDto> GetLeavingReasons();
    }
}
