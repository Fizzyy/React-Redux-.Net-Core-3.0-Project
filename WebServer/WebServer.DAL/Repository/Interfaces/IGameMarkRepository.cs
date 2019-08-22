using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IGameMarkRepository
    {
        Task<IEnumerable<object>> GetUsersScores(string Username);

        Task<int> GetCurrentUserScore(string GameID, string Username);

        Task<double> AddScore(GameMark score);

        Task RemoveScore(string GameMarkID);
    }
}
