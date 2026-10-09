
using System.Text;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;

[assembly: OwinStartup(typeof(WebForm.StartUp))]

namespace WebForm
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            string secretKey =
                "MyVeryStrongSecretKeyForJWT123456789";

            byte[] key = Encoding.UTF8.GetBytes(secretKey);

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode =
                        AuthenticationMode.Active,

                    AllowedAudiences = new[]
                    {
                        "StudentAppUsers"
                    },

                    IssuerSecurityKeyProviders = new[]
                    {
                        new SymmetricKeyIssuerSecurityKeyProvider(
                            "StudentRegistrationApp",
                            key)
                    }
                });
        }
    }
}
