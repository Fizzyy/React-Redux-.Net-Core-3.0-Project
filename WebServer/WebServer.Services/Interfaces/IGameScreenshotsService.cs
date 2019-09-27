using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.Services.Interfaces
{
    public interface IGameScreenshotsService
    {
        Task AddGameScreenShots();

        Task<GamesScreenshots> GetGameScreenShots(string GameID);
    }
}
