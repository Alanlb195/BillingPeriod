using BillingPeriod.Models;
using BillingPeriod.Services.Coockie;
using Microsoft.AspNetCore.Mvc;

namespace BillingPeriod.Controllers
{
    public class CoockieController : Controller
    {
        private readonly ICookieService _cookieService;

        public CoockieController(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cookieData = _cookieService.ObtenerInformacion();
            ViewBag.CookieData = cookieData;
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(string Nombre, string Correo, DateTime Expiracion)
        {
            var datos = new UserCookieData
            {
                Nombre = Nombre,
                Correo = Correo,
                Expiracion = Expiracion
            };

            // Guardar la información en la cookie usando el servicio
            _cookieService.GuardarInformacion(datos);

            // Redirigir a la acción Index para mostrar la información guardada
            return RedirectToAction("Index");
        }



    }
}
