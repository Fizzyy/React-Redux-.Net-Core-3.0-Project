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
    public class MoneyKeysService : IMoneyKeysService
    {
        private readonly IMoneyKeysRepository moneyKeysRepository;

        public MoneyKeysService(IMoneyKeysRepository moneyKeysRepository)
        {
            this.moneyKeysRepository = moneyKeysRepository;
        }

        public async Task<decimal> ActivateKey(string Username, string KeyCode)
        {
            var value = await moneyKeysRepository.ActivateKey(Username, KeyCode);
            return value;
        }

        public async Task AddKey(MoneyKeysBll key)
        {
            await moneyKeysRepository.Add(new MoneyKeys
            {
                KeyCode = key.KeyCode,
                Value = key.Value,
                isActive = true
            });
        }
    }
}
