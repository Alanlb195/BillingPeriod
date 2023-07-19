using BillingPeriod.Models;
using Newtonsoft.Json;

namespace BillingPeriod.Services.Login
{
    public class LoginService : ILoginService
    {
        private const string UserDataKey = "UserData";
        private const string SessionExpirationKey = "SessionExpiration";

        private readonly IHttpContextAccessor _contextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void Login(UserData userData)
        {
            try
            {
                string userDataJson = JsonConvert.SerializeObject(userData);
                _contextAccessor.HttpContext.Session.SetString(UserDataKey, userDataJson);

                // Calcular la fecha y hora de expiración de la sesión
                DateTime sessionExpiration = DateTime.Now.AddSeconds(userData.DurationTime);

                _contextAccessor.HttpContext.Session.SetString(SessionExpirationKey, sessionExpiration.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Error trying to generate the session variable", ex);
            }
        }

        public UserData GetUserData()
        {
            string userDataJson = _contextAccessor.HttpContext.Session.GetString(UserDataKey);

            if (userDataJson != null)
            {
                UserData userData = JsonConvert.DeserializeObject<UserData>(userDataJson);
                return userData;
            }

            return null;
        }

        public DateTime GetExpirationDate()
        {
            // Fecha actual mas la duracion
            string sessionExpirationString = _contextAccessor.HttpContext.Session.GetString(SessionExpirationKey);

            DateTime.TryParse(sessionExpirationString, out DateTime sessionExpiration);
            
            return sessionExpiration;
        }

        public void Logout()
        {
            _contextAccessor.HttpContext.Session.Clear();
        }
    }
}
