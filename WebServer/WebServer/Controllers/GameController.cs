using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
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
        [Route("OrderGames")]
        public async Task<IActionResult> OrderGames([FromQuery]string GamePlatform, [FromQuery]string Type, [FromQuery]string TypeValue)
        {
            try
            {
                IEnumerable<GameDescriptionBll> games = await gameService.OrderGames(GamePlatform, Type, TypeValue);
                return Ok(games);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetGamesByRegex")]
        public async Task<IActionResult> GetGamesByRegex([FromQuery]string GamePlatform, [FromQuery]string GameName)
        {
            List<GameDescriptionBll> games = await gameService.GetGamesByRegex(GamePlatform, GameName);
            return Ok(games);
        }

        [HttpGet]
        [Route("GetSameGenreGames")]
        public async Task<IActionResult> GetSameGenreGames([FromQuery]string GameGenre, [FromQuery]string GameID)
        {
            IEnumerable<Game> games = await gameService.GetSameJenreGames(GameGenre, GameID);
            return Ok(games);
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

        [HttpPost]
        [Route("AddGame")]
        public async Task<IActionResult> AddGame([FromBody]GameBll game)
        {
            try
            {
                await gameService.AddGame(game);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateGame")]
        public async Task<IActionResult> UpdateGame([FromBody]GameBll game)
        {
            try
            { 
                await gameService.UpdateGame(game);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteGame/{GameID}")]
        public async Task<IActionResult> RemoveGame(string GameID)
        {
            try
            {
                await gameService.RemoveGame(GameID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}