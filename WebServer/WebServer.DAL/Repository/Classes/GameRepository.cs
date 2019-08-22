using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using System.Linq;

namespace WebServer.DAL.Repository.Classes
{
    public class GameRepository : IGameRepository
    {
        private readonly CommonContext commonContext;

        public GameRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await Task.FromResult(commonContext.Games);
        }

        public async Task<IEnumerable<Game>> GetCurrentPlatformGames(string GamePlatform)
        {
            return await Task.FromResult(commonContext.Games.Where(x => x.GamePlatform == GamePlatform));
        }

        public async Task<Game> GetChosenGame(string GameID)
        {
            var chosengame = await commonContext.Games.FindAsync(GameID);
            if (chosengame != null) return chosengame;
            else return null;
        }
    }
}