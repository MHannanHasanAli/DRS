using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UModule.Startup))]
namespace UModule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }
    }
}
