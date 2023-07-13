using BillingPeriod.Models;

namespace BillingPeriod.Services.Activities
{
    public interface IActivityService
    {
        Task<bool> AddActivity(Activity activity);
        Task<List<Activity>> GetAllActivities();
        Task<List<Activity>> GetAllActivitiesByDate(DateTime date);
    }
}
