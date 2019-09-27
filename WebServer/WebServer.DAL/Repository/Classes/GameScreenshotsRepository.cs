using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;

namespace WebServer.DAL.Repository.Classes
{
    public class GameScreenshotsRepository : IGameScreenshotsRepository
    {
        private readonly CommonContext commonContext;

        public GameScreenshotsRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public Task AddGameScreenShots()
        {
            throw new NotImplementedException();
        }

        public async Task<GamesScreenshots> GetGameScreenShots(string GameID)
        {
            var gameScreenShots = await commonContext.GamesScreenshots.FirstOrDefaultAsync(x => x.GameID == GameID);
            if (gameScreenShots != null) return gameScreenShots;
            return null;
        }
    }
}
