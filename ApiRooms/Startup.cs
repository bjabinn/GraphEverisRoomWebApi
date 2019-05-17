using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Routing;
using System.Web.Optimization;

[assembly: OwinStartup(typeof(ApiRooms.Startup))]

namespace ApiRooms
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigurarOauth(app);
            app.UseWebApi(config);
        }

        private void ConfigurarOauth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions opcionesAutorizacion =
                new OAuthAuthorizationServerOptions()
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/recuperarToken"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                    Provider = new Credentials.AutorizationToken()
            };
            app.UseOAuthAuthorizationServer(opcionesAutorizacion);
            OAuthBearerAuthenticationOptions opcionesBearer =
                new OAuthBearerAuthenticationOptions()
                {
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active
                };
            app.UseOAuthBearerAuthentication(opcionesBearer);

        }
    }
}
