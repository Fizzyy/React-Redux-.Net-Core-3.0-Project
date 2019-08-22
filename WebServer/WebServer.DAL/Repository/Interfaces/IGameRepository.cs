using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGames();

        Task<IEnumerable<Game>> GetCurrentPlatformGames(string GamePlatform);

        Task<Game> GetChosenGame(string GameID);
    }
}
