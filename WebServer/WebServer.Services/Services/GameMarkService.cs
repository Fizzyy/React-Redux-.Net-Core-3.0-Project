using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.DAL.Models;

namespace WebServer.Services.Services
{
    public class GameMarkService : IGameMarkService
    {
        private readonly IGameMarkRepository gameMarkRepository;
        private readonly IGameFinalScoreRepository gameFinalScoreRepository;

        public GameMarkService(IGameMarkRepository gameMarkRepository, IGameFinalScoreRepository gameFinalScoreRepository)
        {
            this.gameMarkRepository = gameMarkRepository;
            this.gameFinalScoreRepository = gameFinalScoreRepository;
        }

        public Task<IEnumerable<object>> GetUsersScores(string Username)
        {
            return gameMarkRepository.GetUsersScores(Username);
        }

        public async Task<double> AddScore(GameMarkBll score)
        {
            var result = await gameMarkRepository.AddScore(new GameMark { Username = score.Username, GameID = score.GameID, Score = score.Score, GameMarkDate = DateTime.Now.Date });
            var obj = await gameFinalScoreRepository.UpdateScore(new GameFinalScores { GameID = score.GameID, GameScore = result });
            return obj.GameScore;
        }

        public Task RemoveScore(string GameMarkID)
        {
            return gameMarkRepository.RemoveScore(GameMarkID);
        }
    }
}
