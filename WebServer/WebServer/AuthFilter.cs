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
    public class AccountRequirement : IAuthorizationRequirement { }


    public class AuthFilter : AuthorizationHandler<AccountRequirement>
    {
        private readonly IRefreshTokensService refreshTokensService;

        public AuthFilter(IRefreshTokensService refreshTokensService)
        {
            this.refreshTokensService = refreshTokensService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AccountRequirement requirement)
        {
            var actionContext = (AuthorizationFilterContext)context.Resource;
            var token = actionContext.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "AccessToken").Value.FirstOrDefault();
            var refreshtoken = actionContext.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "RefreshToken").Value.FirstOrDefault();
          
            if (token == null || refreshtoken == null)
            {
                //actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                context.Fail();
            }

            var claims = TokenService.VerifyToken(token);
            if (claims == null)
            {
                var principal = ClaimsService.GetPrincipalFromExpiredToken(token);

                var username = principal.Identity.Name;
                var role = principal.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
                var balance = principal.FindFirst("UserBalance");

                var savedRefreshToken = await refreshTokensService.GetRefreshToken(username); //retrieve the refresh token from a data store
                if (savedRefreshToken.RefreshToken != refreshtoken)
                {
                    //actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                    context.Fail();
                    //return BadRequest("Invalid refresh token");
                }

                var identity = ClaimsService.GetIdentity(new UserBll { Username = username, UserBalance = decimal.Parse(balance.Value), Role = role.Value });
                var jwttoken = TokenService.CreateToken(identity);
                var newRefreshToken = TokenService.GenerateRefreshToken();

                await refreshTokensService.DeleteRefreshToken(username);
                await refreshTokensService.SaveRefreshToken(username, newRefreshToken);

                object kek = new
                {
                    token = jwttoken,
                    refreshToken = newRefreshToken
                };

            }


            await Task.Yield();
        }
    }
}
