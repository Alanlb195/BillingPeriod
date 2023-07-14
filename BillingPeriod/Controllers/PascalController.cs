using BillingPeriod.Services.Pascal;
using Microsoft.AspNetCore.Mvc;

namespace BillingPeriod.Controllers
{
    public class PascalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GeneratePascalTriangle(int numRows)
        {
            if (numRows > 0)
            {
                var pascalTriangle = PascalTriangleService.GeneratePascalTriangle(numRows);
                return View("Index", pascalTriangle);
            }

            return RedirectToAction("Index");
        }
    }
}
