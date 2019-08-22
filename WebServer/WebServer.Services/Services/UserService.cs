using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRefreshTokensRepository refreshTokensRepository;

        public UserService(IUserRepository userRepository, IRefreshTokensRepository refreshTokensRepository)
        {
            this.userRepository = userRepository;
            this.refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await userRepository.GetUsers();
        }

        public Task AddUser(UserBll user)
        {
            return userRepository.AddUser(new User { Username = user.Username, Password = Encrypting.GetHashFromPassword(user.Password), isUserBanned = false, UserBalance = 0, Role = "User" });
        }

        public async Task<object> CheckUser(UserBll user)
        {
            var founduser = await userRepository.CheckUser(new User { Username = user.Username, Password = Encrypting.GetHashFromPassword(user.Password) });
            if (founduser == null) return null;

            UserBll FoundUserBll = new UserBll
            {
                Username = founduser.Username,
                Password = founduser.Password,
                Role = founduser.Role,
                UserBalance = founduser.UserBalance
            };

            var identity = ClaimsService.GetIdentity(FoundUserBll);
            if (identity == null) return null;

            var jwttoken = TokenService.CreateToken(identity);
            if (jwttoken != null)
            {
                var newRefreshToken = TokenService.GenerateRefreshToken();
                await refreshTokensRepository.SaveRefreshToken(founduser.Username, newRefreshToken);

                var tokens = new
                {
                    token = jwttoken,
                    refreshToken = newRefreshToken
                };
                return tokens;
            }
            return null;
        }

        public Task<decimal> ReturnUserBalance(string Username)
        {
            return userRepository.ReturnUserBalance(Username);
        }

        public Task BanUser(string Username)
        {
            return userRepository.BanUser(Username);
        }
    }
}
