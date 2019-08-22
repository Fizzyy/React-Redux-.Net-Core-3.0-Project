using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IRefreshTokensRepository
    {
        Task<RefreshTokens> GetRefreshToken(string Username);

        Task DeleteRefreshToken(string Username);

        Task SaveRefreshToken(string Username, string newRefreshToken);
    }
}
