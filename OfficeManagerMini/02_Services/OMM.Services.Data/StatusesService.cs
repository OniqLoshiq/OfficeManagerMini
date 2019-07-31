using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OMM.Data;
using OMM.Services.AutoMapper;
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
    }
}
