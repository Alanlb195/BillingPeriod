using BillingPeriod.Models;
using BillingPeriod.Services.Billing;
using Microsoft.AspNetCore.Mvc;

namespace BillingPeriod.Controllers
{
    public class BillingController : Controller
    {
        private readonly IBillingService _billingService;
        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BillingCalendar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BillingCalendar(Period period)
        {
            List<PeriodRow> periodRows = _billingService.GeneratePeriodRows(period);

            return View(periodRows);
        }
    }
}
