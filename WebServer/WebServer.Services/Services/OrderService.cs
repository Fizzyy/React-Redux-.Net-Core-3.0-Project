using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IGameRepository gameRepository;

        public OrderService(IOrderRepository orderRepository, IGameRepository gameRepository)
        {
            this.orderRepository = orderRepository;
            this.gameRepository = gameRepository;
        }

        public async Task<List<UserOrdersBll>> GetPaidOrUnpaidOrders(string Username, bool type)
        {
            var orders = await orderRepository.GetPaidOrUnpaidOrders(Username, type);

            List<UserOrdersBll> ordersBlls = new List<UserOrdersBll>();
            foreach (var order in orders)
            {
                var game = await gameRepository.GetChosenGame(order.GameID);

                ordersBlls.Add(new UserOrdersBll
                {
                    OrderID = order.Id,
                    GameName = game.GameName,
                    GamePlatform = game.GamePlatform,
                    Amount = order.Amount,
                    Username = order.Username,
                    OrderDate = order.OrderDate,
                    TotalSum = order.TotalSum
                });
            }

            return ordersBlls;
        }

        public async Task<List<UserOrdersBll>> GetAllUserOrders(string Username)
        {
            List<UserOrdersBll> userOrders = new List<UserOrdersBll>();

            var foundorders = await orderRepository.GetAllUserOrders(Username);

            if (foundorders != null)
            {
                foreach (var order in foundorders)
                {
                    var game = await gameRepository.GetChosenGame(order.GameID);

                    userOrders.Add(new UserOrdersBll
                    {
                        OrderID = order.Id,
                        GameName = game.GameName,
                        GamePlatform = game.GamePlatform,
                        Amount = order.Amount,
                        Username = order.Username,
                        OrderDate = order.OrderDate,
                        TotalSum = order.TotalSum
                    });
                }
                return userOrders;
            }

            return null;
        }

        public Task AddOrder(OrdersBll order)
        {
            var date = DateTime.Now;
            return orderRepository.AddOrder(new Orders { Username = order.Username, GameID = order.GameID, Amount = order.Amount, OrderDate = date, TotalSum = order.TotalSum, isOrderPaid = false });
        }

        public async Task<List<UserOrdersBll>> RemoveOrders(string Username, string[] orders)
        {
            await orderRepository.RemoveOrders(orders);
            return await GetPaidOrUnpaidOrders(Username, false);
        }

        public async Task<List<UserOrdersBll>> PayForOrders(string Username, string[] orders)
        {
            await orderRepository.PayForOrders(Username, orders);
            return await GetPaidOrUnpaidOrders(Username, false);
        }
    }
}
