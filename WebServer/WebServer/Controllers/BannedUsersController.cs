using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Controllers
{
    [Route("api/BannedUsers")]
    public class BannedUsersController : Controller
    {
        private readonly IBannedUsersService bannedUsersService;

        public BannedUsersController(IBannedUsersService bannedUsersService)
        {
            this.bannedUsersService = bannedUsersService;
        }

        [HttpPost]
        [Route("GrantBan")]
        public async Task<IActionResult> GrantBan([FromBody]BannedUsersBll model)
        {
            await bannedUsersService.GrantBan(model);
            return Ok();
        }

        [HttpDelete]
        [Route("RevokeBan/{Username}")]
        public async Task<IActionResult> RevokeBan(string Username)
        {
            if (Username != null)
            {
                await bannedUsersService.RevokeBan(Username);
                return Ok();
            }
            else return NotFound();
        }


        [HttpPut]
        [Route("UpdateBanInfo")]
        public async Task<IActionResult> UpdateBanInfo([FromBody]BannedUsersBll model)
        {
            await bannedUsersService.UpdateBanInfo(model);
            return Ok();
        }
    }
}
