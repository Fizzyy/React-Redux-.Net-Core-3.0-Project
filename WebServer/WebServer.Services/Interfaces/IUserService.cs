using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();

        Task<UserBll> GetCurrentUser(string Username);

        Task AddUser(UserBll user);

        Task<object> CheckUser(UserBll user);

        Task<decimal> ReturnUserBalance(string Username);
    }
}
