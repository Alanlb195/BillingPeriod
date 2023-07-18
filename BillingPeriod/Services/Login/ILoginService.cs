using BillingPeriod.Models;

namespace BillingPeriod.Services.Login
{
    public interface ILoginService
    {
        void Login(UserData userData);
        UserData GetUserData();
        DateTime GetExpirationDate();
        void Logout();
    }
}
