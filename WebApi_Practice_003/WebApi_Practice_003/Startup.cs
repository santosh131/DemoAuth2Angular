using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using WebApi_Practice_003.Provider;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(WebApi_Practice_003.Startup))]

namespace WebApi_Practice_003
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            ConfigureAuth(app);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                Provider =new CustomAuthProvider(),
                AccessTokenExpireTimeSpan =TimeSpan.FromMinutes(3) ,
                RefreshTokenProvider =new CustomRefreshTokenProvider()
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
