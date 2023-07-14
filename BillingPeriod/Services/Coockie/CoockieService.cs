using BillingPeriod.Models;

namespace BillingPeriod.Services.Coockie
{
    public class CookieService : ICookieService
    {
        private const string CookieName = "UserData";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void GuardarInformacion(UserCookieData datos)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            TimeSpan duracion = datos.Expiracion - DateTime.Now;

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.Add(duracion),
                HttpOnly = true
            };

            httpContext.Response.Cookies.Append(CookieName, SerializeCookieValues(datos), cookieOptions);
        }

        private string SerializeCookieValues(UserCookieData datos)
        {
            return $"{datos.Nombre}|{datos.Correo}|{datos.Expiracion:yyyy-MM-dd HH:mm:ss}";
        }

        public UserCookieData ObtenerInformacion()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (!httpContext.Request.Cookies.TryGetValue(CookieName, out var cookieValue))
            {
                return null;
            }

            var datos = DeserializeCookieValues(cookieValue);

            if (datos == null || datos.Expiracion < DateTime.Now)
            {
                return null;
            }

            return datos;
        }

        private UserCookieData DeserializeCookieValues(string cookieValue)
        {
            var parts = cookieValue.Split('|');

            if (parts.Length != 3)
            {
                return null;
            }

            var nombre = parts[0];
            var correo = parts[1];
            var expiracion = DateTime.ParseExact(parts[2], "yyyy-MM-dd HH:mm:ss", null);

            return new UserCookieData { Nombre = nombre, Correo = correo, Expiracion = expiracion };
        }

    }
}
