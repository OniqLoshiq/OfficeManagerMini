using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Statuses;
using OMM.Services.Data.Common;

namespace OMM.Services.Data
{
    public class StatusesService : IStatusesService
    {
        private readonly OmmDbContext context;

        public StatusesService(OmmDbContext context)
        {
            this.context = context;
        }

        public IQueryable<StatusListDto> GetAllStatuses()
        {
            return this.context.Statuses.To<StatusListDto>();
        }

        public async Task<int> GetStatusIdByNameAsync(string name)
        {
            if(name == null)
            {
                throw new ArgumentNullException(null, ErrorMessages.StatusNullParameter);
            }

            var statusId = await this.context.Statuses.Where(s => s.Name == name).Select(s => s.Id).SingleOrDefaultAsync();

            if(statusId == 0)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.StatusNameNullReference, name));
            }

            return statusId;
        }

        public async Task<string> GetStatusNameByIdAsync(int id)
        {
            var statusName = await this.context.Statuses.Where(s => s.Id == id).Select(s => s.Name).SingleOrDefaultAsync();

            if(statusName == null)
            {
                throw new ArgumentOutOfRangeException(null, string.Format(ErrorMessages.StatusInvalidRange, id));
            }

            return statusName;
        }
    }
}
