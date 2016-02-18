using Microsoft.Owin;
using Owin;
using TimeView.api;

[assembly: OwinStartup(typeof (Startup))]

namespace TimeView.api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}