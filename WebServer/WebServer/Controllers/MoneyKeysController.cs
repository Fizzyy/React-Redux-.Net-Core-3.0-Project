using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Controllers
{
    [Route("api/MoneyKeys")]
    public class MoneyKeysController : Controller
    {
        private readonly IMoneyKeysService moneyKeysService;

        public MoneyKeysController(IMoneyKeysService moneyKeysService)
        {
            this.moneyKeysService = moneyKeysService;
        }

        [HttpPost]
        [Route("AddKey")]
        public async Task<IActionResult> AddKey(MoneyKeysBll model)
        {
            await moneyKeysService.AddKey(model);
            return Ok();
        }

        [HttpPost]
        [Route("ActivateKey")]
        public async Task<IActionResult> ActivateKey([FromQuery]string KeyCode, [FromQuery]string Username)
        {
            if (KeyCode != null)
            {
                var value = await moneyKeysService.ActivateKey(Username, KeyCode);
                return Ok(value);
            }
            return BadRequest();
        }
    }
}
