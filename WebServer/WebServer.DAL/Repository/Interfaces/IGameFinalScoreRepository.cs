using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Classes;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IGameFinalScoreRepository
    {
        Task<double> GetGameScore(string GameID);

        Task<GameFinalScores> UpdateScore(GameFinalScores game);
    }
}
