using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
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
            var isEmployeeValid = await this.context.Users.AnyAsync(e => e.Id == input.EmployeeId);

            if(!isEmployeeValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.EmployeeIdNullReference, input.EmployeeId));
            }

            var isReportValid = await this.context.Reports.AnyAsync(r => r.Id == input.ReportId);

            if(!isReportValid)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.ReportIdNullReference, input.ReportId));
            }

            var activity = input.To<Activity>();

            await this.context.Activities.AddAsync(activity);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteActivityAsync(string id)
        {
            var activity = await this.context.Activities.SingleOrDefaultAsync(a => a.Id == id);

            if(activity == null)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.ActivityIdNullReference, id));
            }

            this.context.Activities.Remove(activity);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditActivityAsync(ActivityEditDto input)
        {
            var activity = await this.context.Activities.SingleOrDefaultAsync(a => a.Id == input.Id);

            if(activity == null)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.ActivityIdNullReference, input.Id));
            }

            activity.Date = DateTime.ParseExact(input.Date, Constants.ACTIVITY_DATETIME_FORMAT, CultureInfo.InvariantCulture);
            activity.Description = input.Description;
            var workingTime = input.WorkingTime.Split(Constants.WORKING_TIME_SPLIT_VALUE, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            activity.WorkingMinutes = workingTime[0] * Constants.MINUTES_IN_HOUR + workingTime[1];

            this.context.Activities.Update(activity);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<T> GetActivityById<T>(string id)
        {
            var activity = this.context.Activities.Where(a => a.Id == id).To<T>();

            if(!activity.Any())
            {
                throw new NullReferenceException(string.Format(ErrorMessages.ActivityIdNullReference, id));
            }

            return activity;
        }

        public IQueryable<ActivityPieDataDto> GetActivitiesByReportId(string reportId)
        {
            return this.context.Activities.Where(a => a.ReportId == reportId).To<ActivityPieDataDto>();
        }
    }
}
