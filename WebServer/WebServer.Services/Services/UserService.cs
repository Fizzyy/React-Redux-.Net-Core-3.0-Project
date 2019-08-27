using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRefreshTokensRepository refreshTokensRepository;

        private readonly IFeedbackService feedbackService;
        private readonly IGameMarkService gameMarkService;
        private readonly IOrderService orderService;

        public UserService(IUserRepository userRepository, IRefreshTokensRepository refreshTokensRepository, IFeedbackService feedbackService, IGameMarkService gameMarkService, IOrderService orderService)
        {
            this.userRepository = userRepository;
            this.refreshTokensRepository = refreshTokensRepository;
            this.feedbackService = feedbackService;
            this.gameMarkService = gameMarkService;
            this.orderService = orderService;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await userRepository.GetUsers();
        }

        public async Task<UserBll> GetCurrentUser(string Username)
        {
            var founduser = await userRepository.GetCurrentUser(Username);
            if (founduser == null) return null;

            UserBll user = new UserBll
            {
                Username = founduser.Username,
                UserBalance = founduser.UserBalance,
                Role = founduser.Role,
                isUserBanned = founduser.isUserBanned,
                Feedbacks = await feedbackService.GetUserFeedback(Username),
                GameMarks = await gameMarkService.GetAllUserScores(Username),
                Orders = await orderService.GetAllUserOrders(Username)
            };
            return user;
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
