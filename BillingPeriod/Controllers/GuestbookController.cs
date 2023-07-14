using Microsoft.AspNetCore.Mvc;

namespace BillingPeriod.Controllers
{
    public class GuestbookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
