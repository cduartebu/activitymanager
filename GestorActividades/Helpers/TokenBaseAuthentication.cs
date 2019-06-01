using GestorActividades.Services.Implementation;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(GestorActividades.Helpers.TokenBaseAuthentication.Startup))]
namespace GestorActividades.Helpers.TokenBaseAuthentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new MyAuthProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = myProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }

        public class MyAuthProvider : OAuthAuthorizationServerProvider
        {

            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                var authService = new AuthService();
                var userdata = authService.ValidateClientAuthentication(context.UserName, context.Password);
                if (userdata != null)
                {                    
                    identity.AddClaim(new Claim(ClaimTypes.Name, userdata.UserName));                    
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    context.Rejected();
                }
            }

        }

    }
}