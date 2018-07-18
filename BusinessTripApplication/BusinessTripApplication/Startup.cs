using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusinessTripApplication.Startup))]
namespace BusinessTripApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
