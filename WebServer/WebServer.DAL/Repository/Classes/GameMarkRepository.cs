using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebServer.DAL.Repository.Classes
{
    public class GameMarkRepository : IGameMarkRepository
    {
        private readonly CommonContext commonContext;

        public GameMarkRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<IEnumerable<GameMark>> GetAllUserScores(string Username)
        {
            var allusersscores = await Task.FromResult(commonContext.GameMarks.Where(x => x.Username == Username));
            if (allusersscores != null) return allusersscores;
            return null;
        }

        public async Task<double> GetCurrentUserScore(string GameID, string Username)
        {
            try
            {
                if (Username == null || Username == "Войти") return 0;
                var avg = await commonContext.GameMarks.FirstOrDefaultAsync(x => x.GameID == GameID && x.Username == Username);
                if (avg == null) return 0;
                return avg.Score;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddScore(GameMark score)
        {
            var AlreadyPutGame = await commonContext.GameMarks.FirstOrDefaultAsync(x => x.Username == score.Username && x.GameID == score.GameID);
            if (AlreadyPutGame == null)
            {
                commonContext.GameMarks.Add(score);
                var game = await commonContext.GameFinalScores.FindAsync(score.GameID);
                game.AmountOfVotes += 1;
            }
            else
            {
                AlreadyPutGame.Score = score.Score;
                AlreadyPutGame.GameMarkDate = score.GameMarkDate;
            }
            await commonContext.SaveChangesAsync();
        }

        public async Task RemoveScore(int GameMarkID)
        {
            var mark = await commonContext.GameMarks.FindAsync(GameMarkID);
            commonContext.Remove(mark);
            var game = await commonContext.GameFinalScores.FindAsync(mark.GameID);
            game.AmountOfVotes -= 1;
            if (game.AmountOfVotes == 0) game.GameScore = 0;
            await commonContext.SaveChangesAsync();
        }
    }
}
