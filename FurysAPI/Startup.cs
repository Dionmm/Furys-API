using System.Web.Http;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;


[assembly: OwinStartup(typeof(FurysAPI.Startup))]

namespace FurysAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            //app.MapSignalR();
            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app);
            app.UseWebApi(config);
        }
    }
}