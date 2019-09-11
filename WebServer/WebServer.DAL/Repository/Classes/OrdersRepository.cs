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

        public async Task<IEnumerable<Orders>> GetPaidOrUnpaidOrders(string Username, bool type)
        {
            var res = await Task.FromResult(commonContext.Orders.Where(x => x.Username == Username && x.isOrderPaid == type));        
            return res;
        }

        public async Task<IEnumerable<Orders>> GetAllUserOrders(string Username)
        {
            var UserOrders = await Task.FromResult(commonContext.Orders.Where(x => x.Username == Username && x.isOrderPaid == false).OrderByDescending(x => x.OrderDate));
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
                if (k != null) commonContext.Orders.Remove(k);
            }
            await commonContext.SaveChangesAsync();
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