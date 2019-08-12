using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Statuses;

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
            return await this.context.Statuses.Where(s => s.Name == name).Select(s => s.Id).SingleOrDefaultAsync();
        }

        public async Task<string> GetStatusNameByIdAsync(int id)
        {
            return await this.context.Statuses.Where(s => s.Id == id).Select(s => s.Name).SingleOrDefaultAsync();
        }
    }
}
