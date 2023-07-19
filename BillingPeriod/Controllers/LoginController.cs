using BillingPeriod.Models;
using BillingPeriod.Services.Captcha;
using BillingPeriod.Services.Login;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BillingPeriod.Controllers
{
    public class LoginController : Controller
    {
        private const string UserDataKey = "UserData";
        private readonly ILoginService _loginService;
        private readonly ICaptchaService _captchaService;

        public LoginController(ILoginService loginService, ICaptchaService captchaService)
        {
            _loginService = loginService;
            _captchaService = captchaService;
        }

        public IActionResult Index()
        {
            // Verificar si el usuario ya tiene una sesión activa
            if (HttpContext.Session.TryGetValue(UserDataKey, out byte[] userDataBytes))
            {
                // Si existe una sesión, redirigir a la página de bienvenida
                return RedirectToAction("WelcomePage");
            }

            // Obtener la preferencia actual del usuario (por defecto, solo números)
            bool useLettersInCaptcha = false;

            // Verificar si el usuario envió el formulario con una nueva preferencia
            if (HttpContext.Request.Method == "POST")
            {
                // Si hay un valor en el campo CaptchaType, actualizamos la preferencia
                if (bool.TryParse(Request.Form["CaptchaType"], out bool newPreference))
                {
                    useLettersInCaptcha = newPreference;
                }
            }

            // Generar y mostrar el CAPTCHA
            GenerateAndShowCaptcha(useLettersInCaptcha);
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserData userData)
        {

            if (!ModelState.IsValid)
            {
                // Obtener la preferencia actual del usuario (por defecto, solo números)
                bool useLettersInCaptcha = false;

                // Verificar si el usuario envió el formulario con una nueva preferencia
                if (HttpContext.Request.Method == "POST")
                {
                    // Si hay un valor en el campo CaptchaType, actualizamos la preferencia
                    if (bool.TryParse(Request.Form["CaptchaType"], out bool newPreference))
                    {
                        useLettersInCaptcha = newPreference;
                    }
                }

                // Generar y mostrar el CAPTCHA
                GenerateAndShowCaptcha(useLettersInCaptcha);
                return View(userData);
            }

            if (!IsCaptchaValid(userData.Captcha))
            {
                ModelState.AddModelError("Captcha", "Texto del CAPTCHA incorrecto.");
                GenerateAndShowCaptcha(true);
                return View(userData);
            }

            _loginService.Login(userData);

            return RedirectToAction("WelcomePage");
        }


        public IActionResult WelcomePage()
        {

            ViewBag.UserData = _loginService.GetUserData();

            ViewBag.ExpirationTime = _loginService.GetExpirationDate();
            

            if (ViewBag.UserData != null)
                return View();

            return RedirectToAction("Index");
        }




        private bool IsCaptchaValid(string userCaptcha)
        {
            // Obtenemos el texto del CAPTCHA generado y almacenado en TempData
            var expectedCaptcha = TempData["CaptchaText"]?.ToString();
            // Si TempData["CaptchaText"] no existe o es nulo, el CAPTCHA no es válido
            if (string.IsNullOrEmpty(expectedCaptcha))
                return false;

            // Comparamos el texto del CAPTCHA ingresado por el usuario con el texto generado
            return userCaptcha == expectedCaptcha;
        }



        private void GenerateAndShowCaptcha(bool useLetters)
        {
            string captchaText = _captchaService.GenerateCaptchaText(6, useLetters);

            TempData["CaptchaText"] = captchaText;
            var captchaImage = _captchaService.GenerateCaptchaImage(captchaText);
            ViewBag.CaptchaImage = Convert.ToBase64String(captchaImage);
        }



        [HttpPost]
        public ActionResult Logout()
        {
            _loginService.Logout();
            return RedirectToAction("Index");
        }
    }
}
