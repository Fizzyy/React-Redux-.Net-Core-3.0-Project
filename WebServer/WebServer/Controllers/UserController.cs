using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.Services;

namespace WebServer.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRefreshTokensService refreshTokensService;

        public UserController(IUserService userService, IRefreshTokensService refreshTokensService)
        {
            this.userService = userService;
            this.refreshTokensService = refreshTokensService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllUsers()
        {
            IEnumerable<User> users = await userService.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody]UserBll user)
        {
            try
            {
                await userService.AddUser(user);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> CheckUser([FromBody]UserBll user)
        {
            try
            {
                var response = await userService.CheckUser(user);
                if (response != null) return Ok(response);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ReturnUserBalance/{Username}")]
        public async Task<IActionResult> ReturnUserBalance(string Username)
        {
            if (Username != null) return Ok(await userService.ReturnUserBalance(Username));
            return NotFound();
        }

        /*[HttpGet]
        [Route("GetUserAccountData/{Username}")]
        public async Task<IActionResult> GetUserAccountData(string Username)
        {

        }*/

        [HttpPost]
        [Route("BanUser/{Username}")]
        public async Task<IActionResult> BanUser(string Username)
        {
            if (Username != null)
            {
                await userService.BanUser(Username);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("SignOut/{Username}")]
        public async Task<IActionResult> SignOutUser(string Username)
        {
            if (Username != null)
            {
                await refreshTokensService.DeleteRefreshToken(Username);
                return Ok();
            }
            else return BadRequest();
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var token = Request.Headers["AccessToken"];
            var refreshToken = Request.Headers["RefreshToken"];

            var principal = ClaimsService.GetPrincipalFromExpiredToken(token);

            var username = principal.Identity.Name;
            var role = principal.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            var balance = principal.FindFirst("UserBalance");

            var savedRefreshToken = await refreshTokensService.GetRefreshToken(username); //retrieve the refresh token from a data store
            if (savedRefreshToken.RefreshToken != refreshToken) return BadRequest("Invalid refresh token");

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

            return Ok(kek);
        }
    }
}
