using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IE_TaskTracker.Startup))]
namespace IE_TaskTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
