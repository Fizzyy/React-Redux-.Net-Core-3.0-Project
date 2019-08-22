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

        public async Task<IEnumerable<object>> GetUsersScores(string Username)
        {
            var allusersscores = await Task.FromResult(commonContext.GameMarks.Where(x => x.Username == Username)
                                                                              .Join(commonContext.Games,
                                                                                    p => p.GameID,
                                                                                    c => c.GameID,
                                                                                    (p,c) => new
                                                                                    {
                                                                                        GameName = c.GameName,
                                                                                        Score = p.Score,
                                                                                        ScoreID = p.Id,
                                                                                        GamePlatform = c.GamePlatform
                                                                                    }));
            return allusersscores;
        }

        public async Task<int> GetCurrentUserScore(string GameID, string Username)
        {
            try
            {
                if (Username == null || Username == "Войти") return 0;
                var avg = await commonContext.GameMarks.FirstOrDefaultAsync(x => x.GameID == GameID && x.Username == Username);
                return avg.Score;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<double> AddScore(GameMark score)
        {
            var AlreadyPutGame = await commonContext.GameMarks.FirstOrDefaultAsync(x => x.Username == score.Username && x.GameID == score.GameID);
            if (AlreadyPutGame == null) commonContext.GameMarks.Add(score);
            else
            {
                AlreadyPutGame.Score = score.Score;
                AlreadyPutGame.GameMarkDate = score.GameMarkDate;
            }
            await commonContext.SaveChangesAsync();
            var result = await commonContext.GameMarks.Where(x => x.GameID == score.GameID).AverageAsync(x => x.Score);
            return result;
        }

        public async Task RemoveScore(string GameMarkID)
        {
            var mark = await commonContext.GameMarks.FindAsync(GameMarkID);
            commonContext.Remove(mark);
            await commonContext.SaveChangesAsync();
        }
    }
}
