using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APIM.DR
{
    public class AuthClient
    {
        public static AuthenticationResult GetToken()
        {
            AuthenticationResult result = null;

            var context = new AuthenticationContext(string.Format("https://login.windows.net/{0}/oauth2/authorize?api-version=1.0", APIM.Config.Config.TenantId));

            var thread = new Thread(() =>
            {
                    result = context.AcquireToken(
                      "https://management.core.windows.net/",
                      APIM.Config.Config.AppId,
                      new Uri(APIM.Config.Config.RedirectUrl));
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Name = "AquireTokenThread";
            thread.Start();
            thread.Join();

            return result;
        }
    }
}
