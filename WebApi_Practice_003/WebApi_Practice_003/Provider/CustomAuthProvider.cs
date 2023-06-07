//Refer FoodProductsController class for implementation notes
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebApi_Practice_003.Provider
{
    public class CustomAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            if (context.UserName == "admin" && context.Password == "test")
            { 
                var authProp = new AuthenticationProperties(new Dictionary<string, string>
                                {
                                    { "as:client_id", context.ClientId } //used by CustomRefreshTokenProvider and GrantRefreshToken
                                });
                ClaimsIdentity ci = new ClaimsIdentity(context.Options.AuthenticationType);
                ci.AddClaim(new Claim("UserName", context.UserName));
                var authTicket = new AuthenticationTicket(ci, authProp);
                context.Validated(authTicket);
            }
            else
            {
                context.Rejected();
            }

        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;

            if (!context.TryGetFormCredentials(out clientId, out clientSecret))
            {
                context.SetError("invalid_client", "Client credentials could not be retrieved through the Authorization header.");
                context.Rejected();
                return;
            }
            if (clientId == "App1")
            {
                context.OwinContext.Set<string>("as:client_id", clientId); //used in GrantRefreshToken function
                context.Validated(clientId);
            }
            else
            {
                context.Rejected();
            } 
        }

        //First ReceiveAsync in CustomRefreshTokenProvider is called and ticket is set to context in that function
        //ClientId is verified for Current and Original
        //New Ticket is created based on received ticket and added to context 
        //After this CreateAsync in CustomRefreshTokenProvider is called
        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var origClientId = context.Ticket.Properties.Dictionary["as:client_id"];
            var currClientId = context.OwinContext.Get<string>("as:client_id");
            if(origClientId!=currClientId)
            {
                context.Rejected();
                return;
            }

            var refreshIdentity = new ClaimsIdentity(context.Ticket.Identity);
            refreshIdentity.AddClaim(new Claim("newclaim","refreshtoken")); 
            
            var authTicket = new AuthenticationTicket(refreshIdentity,context.Ticket.Properties);
            context.Validated(authTicket); 
        }

        public override async Task MatchEndpoint(OAuthMatchEndpointContext context)
        {

        }
    }
}