using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;

namespace WebServer.DAL.Repository.Classes
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly CommonContext commonContext;

        public MessagesRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<IEnumerable<Rooms>> GetAllRooms()
        {
            return await Task.FromResult(commonContext.Rooms);
        }

        public async Task AddMessage(Messages messages)
        {
            commonContext.Messages.Add(messages);
            await commonContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Messages>> GetAllMessagesFromRoom(string RoomID)
        {
            var comments = await Task.FromResult(commonContext.Messages.Where(x => x.RoomID == RoomID));
            return comments;
        }
    }
}
