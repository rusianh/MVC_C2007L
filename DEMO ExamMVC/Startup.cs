using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DEMO_ExamMVC.Startup))]
namespace DEMO_ExamMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
