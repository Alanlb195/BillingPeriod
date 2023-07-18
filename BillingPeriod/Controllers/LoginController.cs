using BillingPeriod.Models;
using BillingPeriod.Services.Login;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BillingPeriod.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult WelcomePage()
        {

            ViewBag.UserData = _loginService.GetUserData();

            ViewBag.ExpirationTime = _loginService.GetExpirationDate();
            

            if (ViewBag.UserData != null)
                return View();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Index(UserData userData)
        {

            if (!ModelState.IsValid)
                return View(userData);

            _loginService.Login(userData);

            return RedirectToAction("WelcomePage");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _loginService.Logout();
            return RedirectToAction("Index");
        }

        



    }
}
