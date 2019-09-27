using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class Participants
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }

        [ForeignKey("Room")]
        public string RoomName { get; set; }

        public User User { get; set; }

        public Rooms Room { get; set; }
    }
}
