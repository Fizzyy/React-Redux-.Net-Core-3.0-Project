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
            return await Task.FromResult(commonContext.Users.Where(x => x.Role == "User"));
        }

        public async Task<User> GetCurrentUser(string Username)
        {
            var user = await commonContext.Users.FindAsync(Username);
            if (user != null) return user;
            return null;
        }

        public async Task UpdateAvatar(string Username, string AvatartLink)
        {
            var user = await commonContext.Users.FindAsync(Username);
            if (user != null) user.UserImage = AvatartLink;
            await commonContext.SaveChangesAsync();
        }

        public async Task<string> ResetPassword(string Username, string OldPassword, string NewPassword)
        {
            var user = await commonContext.Users.FirstOrDefaultAsync(x => x.Username == Username && x.Password == OldPassword);
            if (user != null)
            {
                user.Password = NewPassword;
                await commonContext.SaveChangesAsync();
                return "Ok";
            }
            return null;
        }

        public async Task AddUser(User user)
        {
            bool flag = false;

            foreach (var person in commonContext.Users)
            {
                if (person.Username.ToLower() == user.Username.ToLower())
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                commonContext.Rooms.Add(new Rooms { RoomName = $"{user.Username}Admin" });
                commonContext.Participants.Add(new Participants { RoomName = $"{user.Username}Admin", Username = "Admin" });
                commonContext.Participants.Add(new Participants { RoomName = $"{user.Username}Admin", Username = user.Username });
                commonContext.Users.Add(user);
            }

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
    }
}
