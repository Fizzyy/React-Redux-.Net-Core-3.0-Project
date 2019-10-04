using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor httpContextAccessor;
        protected string role;

        public AuthFilter() { }

        public AuthFilter(IRefreshTokensService refreshTokensService, IHttpContextAccessor httpContextAccessor)
        {
            this.refreshTokensService = refreshTokensService;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AccountRequirement requirement)
        {
            try
            { 
                string jwtToken, refreshtoken;

                HttpContext httpContext = httpContextAccessor.HttpContext;
                jwtToken = httpContext.Request.Headers["AccessToken"];
                refreshtoken = httpContext.Request.Headers["RefreshToken"];

                //if (httpContext.Request.Headers["Authorize"] == null)
                //{
                //    context.Fail();
                //    httpContext.Response.StatusCode = 401;
                //    return;
                //}

                if (jwtToken == "null" || refreshtoken == "null")
                {
                    context.Fail();
                    httpContext.Response.StatusCode = 401;
                    return;
                }

                var claims = TokenService.VerifyToken(jwtToken);

                if (claims == null)
                {
                    var principal = ClaimsService.GetPrincipalFromExpiredToken(jwtToken);

                    var username = principal.Identity.Name;
                    var role = principal.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                    var balance = principal.FindFirst("UserBalance").Value;

                    var savedRefreshToken = await refreshTokensService.GetRefreshToken(username);
                    //if (savedRefreshToken.RefreshToken != refreshtoken)
                    //{
                    //    context.Fail();
                    //    httpContext.Response.StatusCode = 401;
                    //    return;
                    //}

                    var identity = ClaimsService.GetIdentity(new UserBll
                    {
                        Username = username,
                        UserBalance = decimal.Parse(balance),
                        Role = role
                    });

                    var token = TokenService.CreateToken(identity);
                    var newRefreshToken = TokenService.GenerateRefreshToken();
                    await refreshTokensService.DeleteRefreshToken(username);
                    await refreshTokensService.SaveRefreshToken(username, newRefreshToken);

                    var propertyInfo = token.GetType().GetProperty("access_token");
                    string temp = (string)propertyInfo.GetValue(token, null);

                    httpContext.Response.Headers["AccessToken"] = temp;
                    httpContext.Request.Headers["Authorization"] = "Bearer " + temp;
                    httpContext.Response.Headers["RefreshToken"] = newRefreshToken;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            context.Succeed(requirement);
            await Task.Yield();
        }
    }
}