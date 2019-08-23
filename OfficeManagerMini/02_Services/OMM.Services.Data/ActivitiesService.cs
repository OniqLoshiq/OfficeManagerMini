using System.Threading.Tasks;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Activities;

namespace OMM.Services.Data
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly OmmDbContext context;

        public ActivitiesService(OmmDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateActivityAsync(ActivityCreateDto input)
        {
            var activity = input.To<Activity>();

            await this.context.Activities.AddAsync(activity);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
