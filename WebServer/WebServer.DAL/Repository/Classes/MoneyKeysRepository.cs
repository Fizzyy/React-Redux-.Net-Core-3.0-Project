using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;

namespace WebServer.DAL.Repository.Classes
{
    public class MoneyKeysRepository : IMoneyKeysRepository
    {
        private readonly CommonContext commonContext;

        public MoneyKeysRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task Add(MoneyKeys key)
        {
            commonContext.MoneyKeys.Add(key);
            await commonContext.SaveChangesAsync();
        }

        public async Task<decimal> ActivateKey(string Username, string KeyCode)
        {
            var keycode = await commonContext.MoneyKeys.FirstOrDefaultAsync(x => x.KeyCode == KeyCode && x.isActive == true);
            if (keycode != null)
            {
                keycode.isActive = false;
                var user = commonContext.Users.Find(Username);
                if (user != null)
                {
                    user.UserBalance += keycode.Value;
                }
                await commonContext.SaveChangesAsync();
                return keycode.Value;
            }
            return 0;
        }
    }
}
