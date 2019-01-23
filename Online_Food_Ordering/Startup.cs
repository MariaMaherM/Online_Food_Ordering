using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Online_Food_Ordering.Startup))]
namespace Online_Food_Ordering
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          //  ConfigureAuth(app);
        }
    }
}
