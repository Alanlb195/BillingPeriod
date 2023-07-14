using BillingPeriod.Models;
using BillingPeriod.Services.PresentationCardService;
using Microsoft.AspNetCore.Mvc;

namespace BillingPeriod.Controllers
{
    public class PresentationCardController : Controller
    {
        private readonly IPresentationCardService _presentationCardService;

        public PresentationCardController(IPresentationCardService presentationCardService)
        {
            _presentationCardService = presentationCardService;
        }

        public IActionResult Index(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                ViewData["ImagePath"] = imagePath;
            }

            return View();
        }



        public IActionResult StorePresentationCard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StorePresentationCard(PresentationCard presentationCard)
        {
            if (ModelState.IsValid)
            {
                string imagePath = _presentationCardService.SavePresentationCardAsImage(presentationCard);

                if (!string.IsNullOrEmpty(imagePath))
                {
                    return RedirectToAction("Index", new { imagePath });
                }
                else
                {
                    // Manejar el error en caso de que no se pueda guardar la tarjeta de presentación como imagen
                    // Puedes redirigir a una página de error o mostrar un mensaje de error en la vista
                    ModelState.AddModelError(string.Empty, "Error al guardar la tarjeta de presentación.");
                }
            }

            return View();
        }
    }
}
