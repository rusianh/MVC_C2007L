using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Practice8._1.Startup))]
namespace Practice8._1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
