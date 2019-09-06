using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Services
{
    public class BannedUsersService : IBannedUsersService
    {
        private readonly IBannedUsersRepository bannedUsersRepository;
        
        public BannedUsersService(IBannedUsersRepository bannedUsersRepository)
        {
            this.bannedUsersRepository = bannedUsersRepository;
        }

        public async Task GrantBan(BannedUsersBll bannedUser)
        {
            DateTime date = DateTime.Now;
            await bannedUsersRepository.GrantBan(new BannedUsers
            {
                Username = bannedUser.Username,
                BanReason = bannedUser.BanReason,
                BanDate = date
            });
        }

        public async Task RevokeBan(string Username)
        {
            await bannedUsersRepository.RevokeBan(Username);
        }

        public async Task UpdateBanInfo(BannedUsersBll bannedUser)
        {
            await bannedUsersRepository.UpdateBanInfo(new BannedUsers
            {
                Username = bannedUser.Username,
                BanReason = bannedUser.BanReason,
                BanDate = bannedUser.BanDate
            });
        }
    }
}
