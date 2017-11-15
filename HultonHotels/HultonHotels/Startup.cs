using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HultonHotels.Startup))]
namespace HultonHotels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
