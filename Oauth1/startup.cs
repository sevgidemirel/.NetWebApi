using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using blog.oauth1;
using blog.oauth1.providers;


[assembly: OwinStartup(typeof(blog.oauth1.Startup))]
namespace blog.oauth1
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            ConfigureOAuth(appBuilder);
            blog.WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }

        private void ConfigureOAuth(IAppBuilder appBuilder)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"), 
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                Provider = new SimpleAuthorizationServerProvider() };

            appBuilder.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}