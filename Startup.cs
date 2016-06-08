using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkoutLogPro.Startup))]
namespace WorkoutLogPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
