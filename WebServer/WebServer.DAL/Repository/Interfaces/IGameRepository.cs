using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetCurrentPlatformGames(string GamePlatform);

        Task<IEnumerable<Game>> GetAllGames();

        Task<List<Game>> GetGamesByRegex(string Gameplatform, string GameName);

        Task<IEnumerable<Game>> GetSameJenreGames(string GameGenre, string GameID); 

        Task AddGame(Game game);

        Task<Game> GetChosenGame(string GameID);

        Task UpdateGame(Game game);

        Task RemoveGame(string GameID);
    }
}
