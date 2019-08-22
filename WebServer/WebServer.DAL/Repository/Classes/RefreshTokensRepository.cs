using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;

namespace WebServer.DAL.Repository.Classes
{
    public class RefreshTokensRepository : IRefreshTokensRepository
    {
        private readonly CommonContext commonContext;

        public RefreshTokensRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<RefreshTokens> GetRefreshToken(string Username)
        {
            var RefreshToken = await commonContext.RefreshTokens.FindAsync(Username);
            if (RefreshToken != null) return RefreshToken;
            return null;
        }

        public async Task DeleteRefreshToken(string Username)
        {
            var RefreshToken = await commonContext.RefreshTokens.FindAsync(Username);
            if (RefreshToken != null) commonContext.Remove(RefreshToken);
            await commonContext.SaveChangesAsync();
        }

        public async Task SaveRefreshToken(string Username, string newRefreshToken)
        {
            RefreshTokens refreshToken = new RefreshTokens { Username = Username, RefreshToken = newRefreshToken };
            commonContext.RefreshTokens.Add(refreshToken);
            await commonContext.SaveChangesAsync();
        }
    }
}
