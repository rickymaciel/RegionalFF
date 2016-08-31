using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RegionalFF.Startup))]
namespace RegionalFF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
