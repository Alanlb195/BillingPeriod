using BillingPeriod.Helpers;
using BillingPeriod.Models;
using Microsoft.AspNetCore.Mvc;
using static BillingPeriod.Helpers.PeriodRowCalculator;

namespace BillingPeriod.Controllers
{
    public class BillingController : Controller
    {
        private readonly PeriodRowCalculator periodRowCalculator;

        public BillingController()
        {
            periodRowCalculator = new DefaultPeriodRowCalculator();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateBillingCalendar(Period period)
        {
            List<PeriodRow> periodRows = periodRowCalculator.GeneratePeriodRows(period);

            return RedirectToAction("Index", periodRows);
        }
    }
}
