using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
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
        public async Task<IActionResult> GetCurrentOrders(string Username)
        {
            //var userTokens = TokenService.VerifyToken(Request.Headers["AccessToken"]);
            //if (userTokens == null) return Unauthorized();

            IEnumerable<object> userorders = await orderService.GetCurrentOrders(Username);
            return Ok(userorders);
        }

        [HttpGet]
        [Route("GetPaidOrders/{Username}")]
        public async Task<IActionResult> GetPaidOrders(string Username)
        {
            IEnumerable<object> paidorders = await orderService.GetPaidOrders(Username);
            return Ok(paidorders);
        }

        [HttpPost]
        [Route("AddOrder")]
        //[Authorize (Roles = "User")]
        public async Task<IActionResult> AddOrder([FromBody] OrdersBll model)
        {
            try
            {
                var userTokens = TokenService.VerifyToken(Request.Headers["AccessToken"]);
                if (userTokens == null) return Unauthorized();

                await orderService.AddOrder(model);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteOrders")]
        public async Task<IActionResult> DeleteOrders([FromBody] string[] selectedOrders)
        {
            try
            {
                await orderService.RemoveOrders(selectedOrders);
                return Ok("Deleted!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("PayForOrders/{Username}")]
        public async Task<IActionResult> PayForOrders([FromBody]string[] selectedOrders, string Username)
        {
            try
            {
                await orderService.PayForOrders(Username, selectedOrders);
                return Ok("Paid!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}