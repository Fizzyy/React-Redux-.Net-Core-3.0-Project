using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGames();

        Task<List<GameDescriptionBll>> GetCurrentPlatformGames(string GamePlatform);

        Task<GameDescriptionBll> GetChosenGame(string GameID, string Username);
    }
}
