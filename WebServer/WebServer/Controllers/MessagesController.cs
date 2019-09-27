using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebServer.DAL.Models;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Controllers
{
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        private readonly IMessagesService messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        [HttpGet]
        [Route("GetMessagesFromRoom/{Username}")]
        public async Task<IActionResult> GetMessagesFromRoom(string Username)
        {
            if (Username != null)
            {
                IEnumerable<Messages> messages = await messagesService.GetAllMessagesFromRoom(Username);
                string RoomID = await messagesService.GetRoomID(Username);
                return Ok(new 
                { 
                    roomID = RoomID,
                    messages = messages
                });
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetRooms")]
        public async Task<IActionResult> GetRooms()
        {
            List<RoomBll> Rooms = await messagesService.GetAllRooms();
            return Ok(Rooms);
        }
    }
}
