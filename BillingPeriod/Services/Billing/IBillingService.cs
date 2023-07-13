using BillingPeriod.Models;

namespace BillingPeriod.Services.Billing
{
    public interface IBillingService
    {
        List<PeriodRow> GeneratePeriodRows(Period period);
    }
}
