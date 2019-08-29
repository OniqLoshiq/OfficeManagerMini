using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public class ReportsService : IReportsService
    {
        private readonly OmmDbContext context;

        public ReportsService(OmmDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateReportAsync(string projectId)
        {
            var isProjectIdValid = await this.context.Projects.AnyAsync(p => p.Id == projectId);

            if(!isProjectIdValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.ProjectIdNullReference, projectId));
            }

            if(await this.context.Reports.AnyAsync(r => r.ProjectId == projectId))
            {
                throw new ArgumentException(string.Format(ErrorMessages.ReportInvalidProjectId, projectId));
            }

            var report = new Report { ProjectId = projectId };

            this.context.Reports.Add(report);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<T> GetReportById<T>(string id)
        {
            var report =  this.context.Reports.Where(r => r.Id == id).To<T>();

            if(!report.Any())
            {
                throw new NullReferenceException(string.Format(ErrorMessages.ReportIdNullReference, id));
            }

            return report;
        }
    }
}
