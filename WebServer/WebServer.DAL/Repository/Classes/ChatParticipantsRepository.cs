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
    public class ChatParticipantsRepository : IChatParticipantsRepository
    {
        private readonly CommonContext commonContext;

        public ChatParticipantsRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<User> GetUserFromRoom(string RoomID)
        {
            var Usernames = await commonContext.Participants.FirstOrDefaultAsync(x => x.RoomName == RoomID && x.Username != "Admin");

            var user = await commonContext.Users.FindAsync(Usernames.Username);
            if (user != null) return user;
            return null;
        }

        public async Task<string> FindRoomID(string Username)
        {
            var RoomID = await commonContext.Participants.FirstOrDefaultAsync(x => x.Username != "Admin" && x.Username == Username);
            return RoomID.RoomName;
        }
    }
}
