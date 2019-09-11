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

        Task<IEnumerable<Feedback>> GetUserFeedback(string Username);

        Task AddFeedback(Feedback feedback);

        Task UpdateComment(Feedback feedback);

        Task RemoveFeedback(int FeedbackID);
    }
}
