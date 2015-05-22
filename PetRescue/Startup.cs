using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetRescue.Startup))]
namespace PetRescue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
