using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Interfaces
{
    public interface IMessagesService
    {
        Task<List<RoomBll>> GetAllRooms();

        Task<IEnumerable<Messages>> GetAllMessagesFromRoom(string Username);

        Task<string> GetRoomID(string Username);

        Task AddMessage(MessagesBll messages);
    }
}
