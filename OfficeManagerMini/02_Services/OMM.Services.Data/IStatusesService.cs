using OMM.Services.Data.DTOs.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OMM.Services.Data
{
    public interface IStatusesService
    {
        IQueryable<StatusListDto> GetAllStatuses();
    }
}
