using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Security.Principal;
using mcare.API.Helpers;
using mcare.MongoData.Interface;
using SimpleInjector;
using System.Web.Http;

namespace mcare.API.Custom
{
    public class TokenAuthentication : Attribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, System.Threading.CancellationToken cancellationToken)
        {
            var container = new Container();
            var userTokenRepository = (IUserTokenRepository)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserTokenRepository));
            var request = context.Request;
            var querystrings = request.GetQueryStrings();
            if (querystrings.Any(b => b.Key == "api_key"))
            {
                var token = querystrings["api_key"];
                //if (!bC.IsApiKeyExist(querystrings["api_key"]))
                if (!await userTokenRepository.IsTokenValid(token))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid api key.", request);
                }
                else
                {
                    IPrincipal genericPrincipal = new GenericPrincipal(new GenericIdentity("FFClient"), new string[] { "Admin", "PowerUser" });
                    context.Principal = genericPrincipal;
                }
            }
            else
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid api key.", request);
            }
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, System.Threading.CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                IPrincipal incomingPrincipal = context.ActionContext.RequestContext.Principal;
            });
        }

        public bool AllowMultiple
        {
            get { return false; }
        }

    }
}