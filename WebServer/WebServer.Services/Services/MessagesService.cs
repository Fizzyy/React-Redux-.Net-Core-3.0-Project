using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository messagesRepository;
        private readonly IChatParticipantsRepository chatParticipantsRepository;

        public MessagesService(IMessagesRepository messagesRepository, IChatParticipantsRepository chatParticipantsRepository)
        {
            this.chatParticipantsRepository = chatParticipantsRepository;
            this.messagesRepository = messagesRepository;
        }

        public async Task<List<RoomBll>> GetAllRooms()
        {
            var RoomsList = new List<RoomBll>();

            var rooms = await messagesRepository.GetAllRooms();

            foreach (var room in rooms)
            {
                var user = await chatParticipantsRepository.GetUserFromRoom(room.RoomName);

                RoomsList.Add(new RoomBll
                {
                    RoomName = room.RoomName,
                    Username = user.Username,
                    UserImage = user.UserImage
                });
            }

            return RoomsList;
        }

        public async Task<IEnumerable<Messages>> GetAllMessagesFromRoom(string Username)
        {
            var RoomID = await chatParticipantsRepository.FindRoomID(Username);
            return await messagesRepository.GetAllMessagesFromRoom(RoomID);
        }

        public async Task AddMessage(MessagesBll messages)
        {
            await messagesRepository.AddMessage(new Messages
            {
                RoomID = messages.RoomID,
                Username = messages.Username,
                MessageText = messages.MessageText
            });
        }

        public async Task<string> GetRoomID(string Username)
        {
            return await chatParticipantsRepository.FindRoomID(Username);
        }
    }
}
