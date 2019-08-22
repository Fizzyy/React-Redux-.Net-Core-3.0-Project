using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Controllers
{
    [Route("api/Game")]
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        [Route("GetAllGames")]
        public async Task<IActionResult> GetAllGames()
        {
            IEnumerable<Game> allgames = await gameService.GetAllGames();
            return Ok(allgames);
        }

        [HttpGet]
        [Route("GetCurrentPlatformGames/{GamePlatform}")]
        public async Task<IActionResult> GetCurrentPlatformGames(string GamePlatform)
        {
            if (GamePlatform != null)
            {
                IEnumerable<GameDescriptionBll> currentplatformgames = await gameService.GetCurrentPlatformGames(GamePlatform);
                return Ok(currentplatformgames);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetChosenGame/{GameID}/{Username}")]
        public async Task<IActionResult> GetChosenGame(string GameID, string Username)
        {
            try
            {
                var gameDesc = await gameService.GetChosenGame(GameID, Username);
                if (gameDesc != null) return Ok(gameDesc);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
