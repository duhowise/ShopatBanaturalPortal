using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopatBanaturalPortal.Startup))]
namespace ShopatBanaturalPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
