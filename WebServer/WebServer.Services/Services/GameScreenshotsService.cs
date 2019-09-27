using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;

namespace WebServer.Services.Services
{
    public class GameScreenshotsService : IGameScreenshotsService
    {
        private readonly IGameScreenshotsRepository gameScreenshotsRepository;

        public GameScreenshotsService(IGameScreenshotsRepository gameScreenshotsRepository)
        {
            this.gameScreenshotsRepository = gameScreenshotsRepository;
        }

        public Task AddGameScreenShots()
        {
            throw new NotImplementedException();
        }

        public async Task<GamesScreenshots> GetGameScreenShots(string GameID)
        {
            var GamesScreenshots = await gameScreenshotsRepository.GetGameScreenShots(GameID);
            return GamesScreenshots;
        }
    }
}
