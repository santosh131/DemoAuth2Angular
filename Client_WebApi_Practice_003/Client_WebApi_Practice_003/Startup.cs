using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Client_WebApi_Practice_003.Startup))]
namespace Client_WebApi_Practice_003
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
