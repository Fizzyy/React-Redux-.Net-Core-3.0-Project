using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IGameScreenshotsRepository
    {
        Task AddGameScreenShots();

        Task<GamesScreenshots> GetGameScreenShots(string GameID);
    }
}
