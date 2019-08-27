using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.Services;

namespace WebServer
{
    public class AccountRequirement : IAuthorizationRequirement {

    }

    public class AuthFilter : AuthorizationHandler<AccountRequirement>
    {
        private readonly IRefreshTokensService refreshTokensService;
        private readonly AccountRequirement accountRequirement;
        protected string role;
        public AuthFilter(string role)
        {
            this.role = role;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AccountRequirement requirement)
        {
            var actionContext = (AuthorizationFilterContext)context.Resource;
            var jwtToken = actionContext.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "AccessToken").Value.FirstOrDefault();
            var refreshtoken = actionContext.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "RefreshToken").Value.FirstOrDefault();

            if (jwtToken == null || refreshtoken == null)
            {
                //actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                context.Fail();
            }

            var claims = TokenService.VerifyToken(jwtToken);

            if (actionContext.HttpContext.User.IsInRole(role))
            {
                context.Succeed(requirement);
            }

            if (claims == null)
            {
                var principal = ClaimsService.GetPrincipalFromExpiredToken(jwtToken);

                var username = principal.Identity.Name;
                var role = principal.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
                var balance = principal.FindFirst("UserBalance");

                var savedRefreshToken = await refreshTokensService.GetRefreshToken(username); //retrieve the refresh token from a data store
                if (savedRefreshToken.RefreshToken != refreshtoken)
                {
                    //actionContext.HttpContext.Response.StatusCode = actionContext.HttpContext
                    context.Fail();
                    //return BadRequest("Invalid refresh token");
                }

                var identity = ClaimsService.GetIdentity(new UserBll { Username = username, UserBalance = decimal.Parse(balance.Value), Role = role.Value });
                var token = TokenService.CreateToken(identity); 
                var newRefreshToken = TokenService.GenerateRefreshToken();
                await refreshTokensService.DeleteRefreshToken(username);
                await refreshTokensService.SaveRefreshToken(username, newRefreshToken);
               

                actionContext.HttpContext.Response.Headers.Add("Authorization", new[] { token.GetType().GetField("access_token").ToString(),refreshtoken});
            }


            await Task.Yield();
        }
    }
}
