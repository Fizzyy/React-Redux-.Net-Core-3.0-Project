using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Interfaces
{
    public interface IMoneyKeysService
    {
        Task AddKey(MoneyKeysBll key);

        Task<decimal> ActivateKey(string Username, string KeyCode);
    }
}
