using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class Rooms
    {
        [Key]
        public string RoomName { get; set; }

        public List<Messages> Messages { get; set; }
        public List<Participants> ChatParticipants { get; set; }

        public Rooms()
        {
            Messages = new List<Messages>();
            ChatParticipants = new List<Participants>();
        }
    }
}
