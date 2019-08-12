using OMM.Services.Data.DTOs.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IStatusesService
    {
        IQueryable<StatusListDto> GetAllStatuses();

        Task<int> GetStatusIdByNameAsync(string name);

        Task<string> GetStatusNameByIdAsync(int id);
    }
}
