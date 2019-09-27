using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class Messages
    {
        public int ID { get; set; }

        [ForeignKey("Room")]
        public string RoomID { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }

        public string MessageText { get; set; }

        public User User { get; set; }

        public Rooms Room { get; set; }
    }
}
