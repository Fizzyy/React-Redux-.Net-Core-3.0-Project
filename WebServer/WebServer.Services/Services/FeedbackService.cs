using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IGameRepository gameRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository, IGameRepository gameRepository)
        {
            this.feedbackRepository = feedbackRepository;
            this.gameRepository = gameRepository;
        }

        public Task<List<Feedback>> GetCurrentGameFeedback(string GameID)
        {
            return feedbackRepository.GetCurrentGameFeedback(GameID);
        }

        public async Task<List<UserFeedbackBll>> GetUserFeedback(string Username)
        {
            List<UserFeedbackBll> list = new List<UserFeedbackBll>();

            foreach (var feedback in await feedbackRepository.GetUserFeedback(Username))
            {
                Game game = await gameRepository.GetChosenGame(feedback.GameID);

                list.Add(new UserFeedbackBll
                {
                    CommentID = feedback.Id,
                    Username = Username,
                    Comment = feedback.Comment,
                    CommentDate = feedback.CommentDate,
                    GameName = game.GameName,
                    GamePlatform = game.GamePlatform
                });
            }

            return list;
        }

        public async Task<List<Feedback>> AddFeedback(FeedbackBll feedback)
        {
            var date = DateTime.Now;
            await feedbackRepository.AddFeedback(new Feedback { Username = feedback.Username, GameID = feedback.GameID, Comment = feedback.Comment, CommentDate = date.Date });
            return await feedbackRepository.GetCurrentGameFeedback(feedback.GameID);
        }

        public async Task<List<UserFeedbackBll>> UpdateFeedback(FeedbackBll feedback)
        {
            await feedbackRepository.UpdateComment(new Feedback
            {
                Id = feedback.Id,
                GameID = feedback.GameID,
                Username = feedback.Username,
                Comment = feedback.Comment,
                CommentDate = DateTime.Now.Date,
            });
            return await GetUserFeedback(feedback.Username);
        }

        public async Task<List<UserFeedbackBll>> RemoveFeedback(string Username, int FeedbackID)
        {
            await feedbackRepository.RemoveFeedback(FeedbackID);
            return await GetUserFeedback(Username);
        }
    }
}
