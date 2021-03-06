﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.Mapper;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRefreshTokensRepository refreshTokensRepository;
        private readonly IBannedUsersRepository bannedUsersRepository;

        private readonly IFeedbackService feedbackService;
        private readonly IGameMarkService gameMarkService;
        private readonly IOrderService orderService;

        public UserService(IUserRepository userRepository, IRefreshTokensRepository refreshTokensRepository, IFeedbackService feedbackService, IGameMarkService gameMarkService, IOrderService orderService, IBannedUsersRepository bannedUsersRepository)
        {
            this.userRepository = userRepository;
            this.refreshTokensRepository = refreshTokensRepository;
            this.feedbackService = feedbackService;
            this.gameMarkService = gameMarkService;
            this.orderService = orderService;
            this.bannedUsersRepository = bannedUsersRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await userRepository.GetUsers();
        }

        public async Task<UsersDescriptionBll> GetCurrentUser(string Username)
        {
            var founduser = await userRepository.GetCurrentUser(Username);
            if (founduser == null) return null;

            var banuser = await bannedUsersRepository.FindBannedUser(Username);
            var feedbacks = await feedbackService.GetUserFeedback(Username);
            var orders = await orderService.GetAllUserOrders(Username);
            var gamemarks = await gameMarkService.GetAllUserScores(Username);

            return UserDescriptionMapper.GetUser(founduser, banuser, feedbacks, gamemarks, orders);
        }

        public Task AddUser(UserBll user)
        {
            return userRepository.AddUser(new User 
            { 
                Username = user.Username, 
                Password = Encrypting.GetHashFromPassword(user.Password), 
                UserBalance = 0, 
                Role = "User",
                UserImage = "iTechArt/ylslnjnevinqrik0eql0"
            });
        }

        public async Task<string> ResetPassword(string Username, string OldPassword, string NewPassword)
        {
            var str = await userRepository.ResetPassword(Username, Encrypting.GetHashFromPassword(OldPassword), Encrypting.GetHashFromPassword(NewPassword));
            return str;
        }

        public async Task UpdateAvatar(string Username, string AvatarLink)
        {
             await userRepository.UpdateAvatar(Username, AvatarLink);
        }

        public async Task<object> CheckUser(UserBll user)
        {
            var founduser = await userRepository.CheckUser(new User { Username = user.Username, Password = Encrypting.GetHashFromPassword(user.Password) });
            if (founduser == null) return null;

            var isThisUserBanned = await bannedUsersRepository.FindBannedUser(user.Username);
            if (isThisUserBanned != null)
            {
                object ban = new
                {
                    BanReason = isThisUserBanned.BanReason,
                    BanDate = isThisUserBanned.BanDate
                };

                return ban;
            }

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
    }
}
