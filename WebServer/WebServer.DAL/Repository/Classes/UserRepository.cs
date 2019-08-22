using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebServer.DAL.Repository.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly CommonContext commonContext;

        public UserRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Task.FromResult(commonContext.Users);
        }

        public async Task AddUser(User user)
        {
            commonContext.Users.Add(user);
            await commonContext.SaveChangesAsync();
        }

        public async Task<User> CheckUser(User user)
        {
            var founduser = await commonContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password);
            if (founduser != null) return founduser;
            return null;
        }

        public async Task<decimal> ReturnUserBalance(string Username)
        {
            var user = await commonContext.Users.FindAsync(Username);
            if (user != null) return user.UserBalance;
            return 0;
        }

        public async Task BanUser(string Username)
        {
            var user = await commonContext.Users.FindAsync(Username);
            if (user != null) user.isUserBanned = true;
            await commonContext.SaveChangesAsync();
        }
    }
}
