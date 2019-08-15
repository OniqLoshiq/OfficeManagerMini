using OMM.Data;
using OMM.Domain;
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
            var report = new Report { ProjectId = projectId };

            this.context.Reports.Add(report);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
