using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Task3.Handlers {
    public class BasicAuthHandler: AuthenticationHandler<AuthenticationSchemeOptions> {

        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock systemClock
            //IStudentDbService dbService
            ): base(options, logger, encoder, systemClock) {
            
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
            if(!Request.Headers.ContainsKey("Authorization")){
                return AuthenticateResult.Fail("Missing authorization header");
            }
            // "Authorization: Basic iasdnfinfqenfiqwef" -> "test:dqwfwqef"
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialsBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(":");
            if(credentials.Length < 2) {
                return AuthenticateResult.Fail("Wrong authorization header value");
            }

            //TODO check with the database

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "bob123"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "student")
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name); //basic
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}