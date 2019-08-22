using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using System.Linq;

namespace WebServer.DAL.Repository.Classes
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly CommonContext commonContext;

        public FeedbackRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<List<Feedback>> GetCurrentGameFeedback(string GameID)
        {
            List<Feedback> feedbacks = new List<Feedback>();
            var foundfeedback = await Task.FromResult(commonContext.Feedbacks.Where(x => x.GameID == GameID).OrderByDescending(x => x.CommentDate));
            foreach (var jj in foundfeedback)
            {
                feedbacks.Add(jj);
            }
            return feedbacks;
        }

        public async Task<IEnumerable<object>> GetUserFeedback(string Username)
        {
            var UserFeedback = await Task.FromResult(commonContext.Feedbacks.Where(x => x.Username == Username)
                                                                            .Join(commonContext.Games,
                                                                                  p => p.GameID,
                                                                                  c => c.GameID,
                                                                                  (p, c) => new
                                                                                  {
                                                                                      GameName = c.GameName,
                                                                                      Comment = p.Comment,
                                                                                      CommentDate = p.CommentDate
                                                                                  })
                                                                            .OrderByDescending(x => x.CommentDate));
            return UserFeedback;
        }

        public async Task AddFeedback(Feedback feedback)
        {
            commonContext.Feedbacks.Add(feedback);
            await commonContext.SaveChangesAsync();
        }

        public async Task RemoveFeedback(string FeedbackID)
        {
            var feedback = await commonContext.Feedbacks.FindAsync(int.Parse(FeedbackID));
            commonContext.Remove(feedback);
            await commonContext.SaveChangesAsync();
        }
    }
}
