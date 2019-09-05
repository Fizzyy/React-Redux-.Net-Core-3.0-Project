using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<UserOrdersBll>> GetPaidOrUnpaidOrders(string Username, bool type);

        Task<List<UserOrdersBll>> GetAllUserOrders(string Username);

        Task AddOrder(OrdersBll orders);

        Task<List<UserOrdersBll>> RemoveOrders(string Username, string[] orders);

        Task<List<UserOrdersBll>> PayForOrders(string Username, string[] orders);
    }
}
