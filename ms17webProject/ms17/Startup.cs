using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ms17.Startup))]
namespace ms17
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
