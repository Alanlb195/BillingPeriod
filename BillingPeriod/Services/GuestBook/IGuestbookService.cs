using BillingPeriod.Models;

namespace BillingPeriod.Services.GuestBook
{
    public interface IGuestbookService
    {
        Task<List<Guestbook>> GetAll();
        Task<Guestbook> Get(int id);
        Task<bool> AddGuestbook(Guestbook guestbook);
        Task<bool> UpdateGuestbook(Guestbook guestbook);
        Task<bool> Delete(int Id);
        Task<bool> ExistsGuestbook(int? id);
    }
}
