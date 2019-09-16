using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;

namespace WebServer.DAL.Repository.Classes
{
    public class BannedUsersRepository : IBannedUsersRepository
    {
        private readonly CommonContext commonContext;

        public BannedUsersRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task GrantBan(BannedUsers bannedUser)
        {
            commonContext.BannedUsers.Add(bannedUser);
            await commonContext.SaveChangesAsync();
        }

        public async Task RevokeBan(string Username)
        {
            var user = await commonContext.BannedUsers.FindAsync(Username);
            if (user != null) commonContext.BannedUsers.Remove(user);
            await commonContext.SaveChangesAsync();
        }

        public async Task UpdateBanInfo(BannedUsers bannedUser)
        {
            var user = await commonContext.BannedUsers.FindAsync(bannedUser.Username);
            if (user != null)
            {
                user.BanReason = bannedUser.BanReason;
                user.BanDate = user.BanDate;
            }
            await commonContext.SaveChangesAsync();
        }

        public async Task<BannedUsers> FindBannedUser(string Username)
        {
            var BannedUser = await commonContext.BannedUsers.FirstOrDefaultAsync(x => x.Username == Username);
            if (BannedUser != null) return BannedUser;
            return null;
        }
    }
}
