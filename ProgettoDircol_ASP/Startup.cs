using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProgettoDircol_ASP.Startup))]
namespace ProgettoDircol_ASP
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
