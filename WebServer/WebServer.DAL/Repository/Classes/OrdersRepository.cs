using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using System.Linq;
using WebServer.DAL.Repository.Interfaces;

namespace WebServer.DAL.Repository.Classes
{
    public class OrdersRepository : IOrderRepository
    {
        private readonly CommonContext commonContext;

        public OrdersRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<IEnumerable<object>> GetCurrentOrders(string Username)
        {
            var res = await Task.FromResult(commonContext.Orders
                                            .Where(x => x.isOrderPaid == false)
                                            .Join(commonContext.Games,
                                            p => p.GameID,
                                            c => c.GameID,
                                            (p, c) => new
                                            {
                                                orderID = p.Id,
                                                GameName = c.GameName,
                                                Amount = p.Amount,
                                                OrderDate = p.OrderDate,
                                                GamePlatform = c.GamePlatform,
                                                TotalSum = p.TotalSum
                                            }));
                                            
            return res;
        }


        public async Task<IEnumerable<object>> GetPaidOrders(string Username)
        {
            var res = await Task.FromResult(commonContext.Orders
                                           .Where(x => x.isOrderPaid == true)
                                           .Join(commonContext.Games,
                                           p => p.GameID,
                                           c => c.GameID,
                                           (p, c) => new
                                           {
                                               orderID = p.Id,
                                               GameName = c.GameName,
                                               Amount = p.Amount,
                                               OrderDate = p.OrderDate,
                                               GamePlatform = c.GamePlatform,
                                               TotalSum = p.TotalSum
                                           })
                                           .OrderByDescending(x => x.OrderDate));

            return res;
        }

        public async Task<IEnumerable<Orders>> GetAllUserOrders(string Username)
        {
            var UserOrders = await Task.FromResult(commonContext.Orders.Where(x => x.Username == Username));
            if (UserOrders != null) return UserOrders;
            return null;
        }

        public async Task AddOrder(Orders order)
        {
            commonContext.Orders.Add(order);
            await commonContext.SaveChangesAsync();
        }

        public async Task RemoveOrders(string[] orders)
        {
            for (int i = 0; i < orders.Length; i++)
            {
                var k = await commonContext.Orders.FindAsync(int.Parse(orders[i]));
                if (k != null)
                {
                    commonContext.Orders.Remove(k);
                    await commonContext.SaveChangesAsync();
                }
            }
        }

        public async Task PayForOrders(string Username, string[] orders)
        {
            decimal tempsum = 0;
            for (int i = 0; i < orders.Length; i++)
            {
                var defaultorder = await commonContext.Orders.FindAsync(int.Parse(orders[i]));
                if (defaultorder != null)
                {
                    tempsum += defaultorder.TotalSum;
                    defaultorder.isOrderPaid = true;
                }
            }
            var user = await commonContext.Users.FindAsync(Username);
            if (user != null) user.UserBalance -= tempsum;
            await commonContext.SaveChangesAsync();
        }
    }
}