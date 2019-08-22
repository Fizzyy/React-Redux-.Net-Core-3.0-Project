using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Interfaces
{
    public interface IGameMarkService
    {
        Task<IEnumerable<object>> GetUsersScores(string Username);

        Task<double> AddScore(GameMarkBll score);

        Task RemoveScore(string GameMarkID);
    }
}
