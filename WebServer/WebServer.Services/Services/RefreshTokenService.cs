using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Services
{
    public class RefreshTokenService : IRefreshTokensService
    {
        private readonly IRefreshTokensRepository refreshTokensRepository;

        public RefreshTokenService(IRefreshTokensRepository refreshTokensRepository)
        {
            this.refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<RefreshTokensBll> GetRefreshToken(string Username)
        {
            var res = await refreshTokensRepository.GetRefreshToken(Username);
            if (res == null) return null;

            RefreshTokensBll refreshTokensBll = new RefreshTokensBll
            {
                Username = res.Username,
                RefreshToken = res.RefreshToken
            };

            return refreshTokensBll;
        }

        public async Task DeleteRefreshToken(string Username)
        {
            await refreshTokensRepository.DeleteRefreshToken(Username);
        }

        public async Task SaveRefreshToken(string Username, string newRefreshToken)
        {
            await refreshTokensRepository.SaveRefreshToken(Username, newRefreshToken);
        }
    }
}
