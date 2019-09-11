using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Interfaces
{
    public interface IGameMarkService
    {
        Task<List<UserScoresBll>> GetAllUserScores(string Username);

        Task<double> AddScore(GameMarkBll score);

        Task<List<UserScoresBll>> RemoveScore(string Username, int GameMarkID);
    }
}
