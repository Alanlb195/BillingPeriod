using BillingPeriod.Models;

namespace BillingPeriod.Services
{
    public interface IBillingService
    {
        List<PeriodRow> GeneratePeriodRows(Period period);
    }
}
