using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetCurrentGameFeedback(string GameID);

        Task<List<UserFeedbackBll>> GetUserFeedback(string Username);

        Task<List<Feedback>> AddFeedback(FeedbackBll feedback);

        Task<List<UserFeedbackBll>> UpdateFeedback(FeedbackBll feedback);

        Task<List<UserFeedbackBll>> RemoveFeedback(string Username, int FeedbackID);
    }
}
