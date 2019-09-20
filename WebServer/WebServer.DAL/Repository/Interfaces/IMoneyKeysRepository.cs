using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IMoneyKeysRepository
    {
        Task Add(MoneyKeys key);

        Task<decimal> ActivateKey(string Username, string KeyCode);
    }
}
