using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Controllers
{
    [Route("api/GameMark")]
    public class GameMarkController : Controller
    {
        private readonly IGameMarkService gameMarkService;

        public GameMarkController(IGameMarkService gameMarkService)
        {
            this.gameMarkService = gameMarkService;
        }

        [HttpGet]
        [Route("GetUserMarks/{Username}")]
        public async Task<IActionResult> GetUserMarks(string Username)
        {
            if (Username != null)
            {
                IEnumerable<object> gameMarks =  await gameMarkService.GetUsersScores(Username);
                return Ok(gameMarks);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("AddScore")]
        public async Task<IActionResult> AddScore([FromBody]GameMarkBll model)
        {
            var res = await gameMarkService.AddScore(model);
            return Ok(res);
        }
    }
}
