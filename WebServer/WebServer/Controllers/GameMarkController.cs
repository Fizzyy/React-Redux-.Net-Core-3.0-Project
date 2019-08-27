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

        [HttpPost]
        [Route("AddScore")]
        public async Task<IActionResult> AddScore([FromBody]GameMarkBll model)
        {
            var res = await gameMarkService.AddScore(model);
            return Ok(res);
        }

        [HttpPost]
        [Route("DeleteScore")]
        public async Task<IActionResult> DeleteScore([FromBody]GameMarkBll model)
        {
            await gameMarkService.RemoveScore(model.GameID);
            return Ok();
        }
    }
}
