using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Repository.Interfaces;
using System.Linq;
using WebServer.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WebServer.DAL.Repository.Classes
{
    public class GameFinalScoreRepository : IGameFinalScoreRepository
    {
        private readonly CommonContext commonContext;

        public GameFinalScoreRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<double> GetGameScore(string GameID)
        {
            var score = await commonContext.GameFinalScores.FindAsync(GameID);
            return score.GameScore;
        }

        public async Task<GameFinalScores> UpdateScore(GameFinalScores game)
        {
            var foundgame = await commonContext.GameFinalScores.FindAsync(game.GameID);
            if (foundgame != null)
            {
                foundgame.GameScore = game.GameScore;
            }
            await commonContext.SaveChangesAsync();
            return foundgame;
        }
    }
}
