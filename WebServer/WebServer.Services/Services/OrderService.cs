using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public Task<IEnumerable<object>> GetCurrentOrders(string Username)
        {
            return orderRepository.GetCurrentOrders(Username);
        }

        public Task<IEnumerable<object>> GetPaidOrders(string Username)
        {
            return orderRepository.GetPaidOrders(Username);
        }

        public Task AddOrder(OrdersBll order)
        {
            var date = DateTime.Now.Date;
            return orderRepository.AddOrder(new Orders { Username = order.Username, GameID = order.GameID, Amount = order.Amount, OrderDate = date, TotalSum = order.TotalSum, isOrderPaid = false });
        }

        public Task RemoveOrders(string[] orders)
        {
            return orderRepository.RemoveOrders(orders);
        }

        public Task PayForOrders(string Username, string[] orders)
        {
            return orderRepository.PayForOrders(Username, orders);
        }
    }
}
