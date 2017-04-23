using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projeto_KB.Startup))]
namespace Projeto_KB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
