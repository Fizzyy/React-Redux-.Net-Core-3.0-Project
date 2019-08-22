using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Interfaces
{
    public interface IRefreshTokensService
    {
        Task<RefreshTokensBll> GetRefreshToken(string Username);

        Task DeleteRefreshToken(string Username);

        Task SaveRefreshToken(string Username, string newRefreshToken);
    }
}
