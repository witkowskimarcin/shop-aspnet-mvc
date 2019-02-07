using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sklep.Startup))]
namespace sklep
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
