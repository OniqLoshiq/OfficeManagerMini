using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IReportsService
    {
        Task<bool> CreateReportAsync(string projectId);
    }
}
