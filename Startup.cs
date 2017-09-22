using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NamesRecommender.Startup))]
namespace NamesRecommender
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
