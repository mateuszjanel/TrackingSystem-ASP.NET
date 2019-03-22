using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectTrackingSystem.Startup))]
namespace ProjectTrackingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
