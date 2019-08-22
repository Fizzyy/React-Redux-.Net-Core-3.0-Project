using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetCurrentGameFeedback(string GameID);

        Task<IEnumerable<object>> GetUserFeedback(string Username);

        Task AddFeedback(FeedbackBll feedback);

        Task RemoveFeedback(string FeedbackID);
    }
}
