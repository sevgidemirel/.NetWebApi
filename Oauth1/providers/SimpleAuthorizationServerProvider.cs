using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Security.Claims;
using blog.Models;
using System.Linq;
using System.Web.Http;
using System;


namespace blog.oauth1.providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public blogcontext db = new blogcontext();

        kullanici kul = new kullanici();

        public override async System.Threading.Tasks.Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var query = from kullanici in db.kullanicilar
                        where kullanici.kullaniciAd == context.UserName && kullanici.password == context.Password
                        select kullanici;
            kullanici k = query.FirstOrDefault<kullanici>();
            kul = k;
            int sonuc;
            sonuc = query.Count();
            if (sonuc > 0)
            {
                rol r = new rol();
                IQueryable<rol> query1 = from kullanici in db.kullanicilar
                                         join rol in db.rols
                             on k.kullaniciId equals rol.kullanici.kullaniciId
                                         select rol;
                r = query1.FirstOrDefault<rol>();
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("isim", context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, r.roltype));
                context.Validated(identity);


            }
            else
            {
                context.SetError("invalid_grant", "kul veya şif yanlış");
            }
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            token t = new token();
            t.tknaccess = context.AccessToken;
            t.date = DateTime.Now;
            t.kullanici = kul;
            db.tokens.Add(t);
            db.SaveChanges();
            return base.TokenEndpointResponse(context);
        }
    }
}

//using Microsoft.Owin.Security.OAuth;
//using System.Threading.Tasks;
//using System.Security.Claims;
//using blog.Models;
//using System.Linq;


//namespace blog.oauth1.providers
//{
//    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
//    {
//        public blogcontext db = new blogcontext();
//        public override async System.Threading.Tasks.Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
//        {
//            context.Validated();
//        }

//        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
//        {
//            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
//            var query = from kullanici in db.kullanicilar
//                        where kullanici.kullaniciAd == context.UserName && kullanici.password == context.Password
//                        select kullanici;
//            int sonuc;
//            sonuc = query.Count();
//            kullanici k;
//            k=query.FirstOrDefault<kullanici>(); 
//            if (sonuc > 0)
//            {
//                if ( k.kullaniciAd== "try")
//                {
//                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
//                    identity.AddClaim(new Claim("sub", context.UserName));
//                    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
//                    context.Validated(identity);
//                }
//                else
//                {
//                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
//                    identity.AddClaim(new Claim("sub", context.UserName));
//                    identity.AddClaim(new Claim(ClaimTypes.Role, "junioradmin"));
//                    context.Validated(identity);

//                }
//            }
//            else
//            {  context.SetError("invalid_grant", "kul veya şifre yanlış");
//            }
//        }
//    }
//}