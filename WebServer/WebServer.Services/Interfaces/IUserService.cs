using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();

        Task<UsersDescriptionBll> GetCurrentUser(string Username);

        Task AddUser(UserBll user);

        Task<string> ResetPassword(string Username, string OldPassword, string NewPassword);

        Task UpdateAvatar(string Username, string AvatarLink);

        Task<object> CheckUser(UserBll user);

        Task<decimal> ReturnUserBalance(string Username);
    }
}
