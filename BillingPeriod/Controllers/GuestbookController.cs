using BillingPeriod.Models;
using BillingPeriod.Services.GuestBook;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BillingPeriod.Controllers
{
    public class GuestbookController : Controller
    {
        private readonly IGuestbookService _guestbookService;

        public GuestbookController(IGuestbookService guestbookService)
        {
            _guestbookService = guestbookService;
        }

        [HttpGet]
        public async Task<IActionResult> Default()
        {
            List<Guestbook> guestbooks = await _guestbookService.GetAll();

            return View(guestbooks);
        }


        public async Task<IActionResult> Registro(int id)
        {

            bool exist = await _guestbookService.ExistsGuestbook(id);


            if (exist == true)
            {
                Guestbook guestbook = await _guestbookService.Get(id);

                return View(guestbook);
            }
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Registar(Guestbook guestbook)
        {
            if (ModelState.IsValid)
            {

                bool guestbookExist = await _guestbookService.ExistsGuestbook(guestbook.Id);


                if (guestbookExist == true)
                {
                    bool isUpdated = await _guestbookService.UpdateGuestbook(guestbook);
                    return RedirectToAction("Default");

                }
                else
                {
                    bool isCreated = await _guestbookService.AddGuestbook(guestbook);

                    return RedirectToAction("Default");
                }

            }

            return RedirectToAction("Registro");
        }


        public async Task<IActionResult> Delete(int Id)
        {
            bool isDeleted = await _guestbookService.Delete(Id);

            if (isDeleted == true)
            {
                return RedirectToAction("Default");
            }
            return RedirectToAction("Default");
        }




    }
}
