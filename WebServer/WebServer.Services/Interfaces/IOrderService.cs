using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<object>> GetCurrentOrders(string Username);

        Task<IEnumerable<object>> GetPaidOrders(string Username);

        Task AddOrder(OrdersBll orders);

        Task RemoveOrders(string[] orders);

        Task PayForOrders(string Username, string[] orders);
    }
}
