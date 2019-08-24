using OMM.Services.Data.DTOs.Activities;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IActivitiesService
    {
        Task<bool> CreateActivityAsync(ActivityCreateDto input);

        Task<bool> DeleteActivityAsync(string id);

        IQueryable<T> GetActivityById<T>(string id);

        Task<bool> EditActivityAsync(ActivityEditDto input);
    }
}
