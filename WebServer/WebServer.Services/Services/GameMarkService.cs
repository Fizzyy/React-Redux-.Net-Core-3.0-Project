using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Services
{
    public class GameMarkService : IGameMarkService
    {
        private readonly IGameMarkRepository gameMarkRepository;
        private readonly IGameFinalScoreRepository gameFinalScoreRepository;
        private readonly IGameRepository gameRepository;

        public GameMarkService(IGameMarkRepository gameMarkRepository, IGameFinalScoreRepository gameFinalScoreRepository, IGameRepository gameRepository)
        {
            this.gameMarkRepository = gameMarkRepository;
            this.gameFinalScoreRepository = gameFinalScoreRepository;
            this.gameRepository = gameRepository;
        }

        public async Task<List<UserScoresBll>> GetAllUserScores(string Username)
        {
            List<UserScoresBll> userscores = new List<UserScoresBll>();

            var foundscores = await gameMarkRepository.GetAllUserScores(Username);

            if (foundscores != null)
            {
                foreach (var score in foundscores)
                {
                    var game = await gameRepository.GetChosenGame(score.GameID);
                    userscores.Add(new UserScoresBll
                    {
                        ScoreID = score.Id,
                        Score = score.Score,
                        Username = score.Username,
                        GameName = game.GameName,
                        GamePlatform = game.GamePlatform
                    });
                }
            }
            else return null;
            return userscores;
        }

        public async Task<double> AddScore(GameMarkBll score)
        {
            await gameMarkRepository.AddScore(new GameMark { Username = score.Username, GameID = score.GameID, Score = score.Score, GameMarkDate = DateTime.Now.Date });
            var obj = await gameFinalScoreRepository.UpdateScore(new GameFinalScores { GameID = score.GameID });
            return obj.GameScore;
        }

        public async Task<List<UserScoresBll>> RemoveScore(string Username, int GameMarkID)
        {
            await gameMarkRepository.RemoveScore(GameMarkID);
            return await GetAllUserScores(Username);
        }
    }
}
