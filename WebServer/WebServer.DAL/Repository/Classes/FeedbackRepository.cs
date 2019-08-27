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

        public async Task<IEnumerable<Feedback>> GetUserFeedback(string Username)
        {
            var UserFeedback = await Task.FromResult(commonContext.Feedbacks.Where(x => x.Username == Username));
            if (UserFeedback != null) return UserFeedback;
            return null;
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
