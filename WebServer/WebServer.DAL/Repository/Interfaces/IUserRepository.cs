using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetCurrentUser(string Username);

        Task AddUser(User user);

        Task<User> CheckUser(User user);

        Task<decimal> ReturnUserBalance(string Username);

        Task BanUser(string Username);
    }
}
