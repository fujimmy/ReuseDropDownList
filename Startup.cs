using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReuseDropDownList.Startup))]
namespace ReuseDropDownList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
