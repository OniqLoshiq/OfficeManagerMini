using OMM.Services.Data.DTOs.Activities;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface IActivitiesService
    {
        Task<bool> CreateActivityAsync(ActivityCreateDto input);
    }
}
