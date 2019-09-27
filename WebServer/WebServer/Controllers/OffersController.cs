using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Controllers
{
    [Route("api/Offers")]
    public class OffersController : Controller
    {
        private readonly IOffersService offersService;

        public OffersController(IOffersService offersService)
        {
            this.offersService = offersService;
        }

        [HttpGet]
        [Route("GetOfferGames")]
        public async Task<IActionResult> GetOfferGames()
        {
            var OfferGames = await offersService.GetAllOfferGames();
            return Ok(OfferGames);
        }

        [HttpGet]
        [Route("GetOffersByRegex")]
        public async Task<IActionResult> GetOffersByRegex([FromQuery]string GamePlatform, [FromQuery]string GameName)
        {
            var offergames = await offersService.GetOffersByRegex(GamePlatform, GameName);
            return Ok(offergames);
        }

        [HttpGet]
        [Route("GetOffersFromPlatform/{GamePlatform}")]
        public async Task<IActionResult> GetOffersFromPlatform(string GamePlatform)
        {
            if (GamePlatform != null)
            {
                var offergames = await offersService.GetOffersFromPlatform(GamePlatform);
                return Ok(offergames);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("AddOffer")]
        public async Task<IActionResult> AddOffer([FromBody]OffersBll model)
        {
            await offersService.AddGameOffer(model);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateOffer")]
        public async Task<IActionResult> UpdateOffer([FromBody]OffersBll model)
        {
            await offersService.UpdateOffer(model);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteOffer/{OfferID}")]
        public async Task<IActionResult> DeleteOffer(string OfferID)
        {
            await offersService.DeleteOffer(OfferID);
            return Ok();
        }

        [HttpGet]
        [Route("GetOffer/{OfferID}")]
        public async Task<IActionResult> GetOffer(string OfferID)
        {
            if (OfferID != null)
            {
                Offers offer = await offersService.GetOffer(OfferID);
                return Ok();
            }
            return NotFound();
        }
    }
}
