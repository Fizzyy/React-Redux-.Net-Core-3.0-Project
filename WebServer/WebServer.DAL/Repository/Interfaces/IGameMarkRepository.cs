using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IGameMarkRepository
    {
        Task<IEnumerable<GameMark>> GetAllUserScores(string Username);

        Task<double> GetCurrentUserScore(string GameID, string Username);

        Task AddScore(GameMark score);

        Task RemoveScore(string GameMarkID);
    }
}
