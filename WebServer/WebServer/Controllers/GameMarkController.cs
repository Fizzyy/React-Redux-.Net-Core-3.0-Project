using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

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

        [HttpPost]
        [Route("AddScore")]
        [Authorize(Policy = "MyPolicy")]
        public async Task<IActionResult> AddScore([FromBody]GameMarkBll model)
        {
            var res = await gameMarkService.AddScore(model);
            return Ok(res);
        }

        [HttpDelete]
        [Route("DeleteScore")]
        [Authorize(Policy = "MyPolicy")]
        public async Task<IActionResult> DeleteScore([FromQuery]string Username, [FromQuery]int scoreID)
        {
            List<UserScoresBll> scores = await gameMarkService.RemoveScore(Username, scoreID);
            return Ok(scores);
        }
    }
}
