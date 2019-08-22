using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        public Task<List<Feedback>> GetCurrentGameFeedback(string GameID)
        {
            return feedbackRepository.GetCurrentGameFeedback(GameID);
        }

        public Task<IEnumerable<object>> GetUserFeedback(string Username)
        {
            return feedbackRepository.GetUserFeedback(Username);
        }

        public Task AddFeedback(FeedbackBll feedback)
        {
            var date = DateTime.Now;
            return feedbackRepository.AddFeedback(new Feedback { Username = feedback.Username, GameID = feedback.GameID, Comment = feedback.Comment, CommentDate = date.Date });
        }

        public Task RemoveFeedback(string FeedbackID)
        {
            return feedbackRepository.RemoveFeedback(FeedbackID);
        }
    }
}
