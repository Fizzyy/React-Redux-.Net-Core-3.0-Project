using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;
using WebServer.Services.Services;

namespace WebServer.Controllers
{
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        [Route("GetCurrentOrders/{Username}")]
        [Authorize(Policy = "MyPolicy", Roles = "User")]
        public async Task<IActionResult> GetCurrentOrders(string Username)
        {
            List<UserOrdersBll> userorders = await orderService.GetPaidOrUnpaidOrders(Username, false);
            return Ok(userorders);
        }

        [HttpGet]
        [Route("GetPaidOrders/{Username}")]
        public async Task<IActionResult> GetPaidOrders(string Username)
        {
            List<UserOrdersBll> userorders = await orderService.GetPaidOrUnpaidOrders(Username, true);
            return Ok(userorders);
        }

        [HttpPost]
        [Route("AddOrder")]
        [Authorize(Policy = "MyPolicy")]
        public async Task<IActionResult> AddOrder([FromBody] OrdersBll model)
        {
            try
            {
                await orderService.AddOrder(model);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteOrders")]
        public async Task<IActionResult> DeleteOrders([FromBody]PayOrDeleteOrdersBll orders)
        {
            try
            {
                List<UserOrdersBll> NewOrders =  await orderService.RemoveOrders(orders.Username, orders.orders);
                return Ok(NewOrders);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("PayForOrders")]
        public async Task<IActionResult> PayForOrders([FromBody]PayOrDeleteOrdersBll orders)
        {
            try
            {
                List<UserOrdersBll> NewOrders = await orderService.PayForOrders(orders.Username, orders.orders);
                return Ok(NewOrders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}