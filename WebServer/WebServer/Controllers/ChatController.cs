using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Controllers
{
    public class ChatController : Hub
    {
        private readonly IMessagesService messagesService;

        public ChatController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        public async Task Send(string RoomID, string user, string message)
        {
            await messagesService.AddMessage(new MessagesBll
            {
                RoomID = RoomID,
                Username = user,
                MessageText = message
            });

            await Clients.All.SendAsync("Send", RoomID, user, message);
        }
    }
}
