using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IChatParticipantsRepository
    {
        Task<User> GetUserFromRoom(string RoomID);

        Task<string> FindRoomID(string Username);
    }
}
