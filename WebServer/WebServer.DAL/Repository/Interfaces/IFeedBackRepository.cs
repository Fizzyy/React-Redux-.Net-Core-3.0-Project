using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetCurrentGameFeedback(string GameID);

        Task<IEnumerable<object>> GetUserFeedback(string Username);

        Task AddFeedback(Feedback feedback);

        Task RemoveFeedback(string FeedbackID);
    }
}
