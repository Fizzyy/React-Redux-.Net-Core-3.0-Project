using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IMessagesRepository
    {
        Task<IEnumerable<Rooms>> GetAllRooms();

        Task<IEnumerable<Messages>> GetAllMessagesFromRoom(string RoomID);

        Task AddMessage(Messages messages);
    }
}
