using BillingPeriod.Models;
using Microsoft.EntityFrameworkCore;

namespace BillingPeriod.Services.Activities
{
    public class ActivityService : IActivityService
    {
        private readonly DefaultDBContext _dbContext;

        public ActivityService(DefaultDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddActivity(Activity activity)
        {
            bool isConflict = await CheckActivityConflict(activity);

            if (isConflict)
            {
                return false; // Retorna false si hay conflicto de fechas
            }

            _dbContext.Add(activity);
            await _dbContext.SaveChangesAsync();

            return true; // Retorna true si se agrega exitosamente
        }

        private async Task<bool> CheckActivityConflict(Activity activity)
        {
            bool isConflict = await _dbContext.Activity.AnyAsync(a =>
                (a.InitialDate <= activity.InitialDate && activity.InitialDate < a.FinalDate) ||
                (a.InitialDate < activity.FinalDate && activity.FinalDate <= a.FinalDate) ||
                (activity.InitialDate <= a.InitialDate && a.InitialDate < activity.FinalDate) ||
                (activity.InitialDate < a.FinalDate && a.FinalDate <= activity.FinalDate));

            return isConflict;
        }



        public async Task<List<Activity>> GetAllActivities()
        {
            List<Activity> activities = await _dbContext.Activity.ToListAsync();
            return activities;
        }

        public async Task<List<Activity>> GetAllActivitiesByDate(DateTime date)
        {
            List<Activity> activities = await _dbContext.Activity
                .Where(a => a.InitialDate.Date == date.Date)
                .ToListAsync();

            return activities;
        }


    }
}