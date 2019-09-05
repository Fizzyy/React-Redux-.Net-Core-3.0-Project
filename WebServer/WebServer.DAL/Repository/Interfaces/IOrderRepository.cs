using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Orders>> GetPaidOrUnpaidOrders(string Username, bool type);

        Task<IEnumerable<Orders>> GetAllUserOrders(string Username);

        Task AddOrder(Orders order);

        Task RemoveOrders(string[] orders);

        Task PayForOrders(string Username, string[] orders);
    }
}
