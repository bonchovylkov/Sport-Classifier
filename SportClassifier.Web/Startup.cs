using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportClassifier.Web.Startup))]
namespace SportClassifier.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
