using BillingPeriod.Models;

namespace BillingPeriod.Services.Coockie
{
    public interface ICookieService
    {
        void GuardarInformacion(UserCookieData datos);
        UserCookieData ObtenerInformacion();
    }
}
