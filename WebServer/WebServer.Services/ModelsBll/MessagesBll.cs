using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll
{
    public class MessagesBll
    {
        public int ID { get; set; }

        public string RoomID { get; set; }

        public string Username { get; set; }

        public string MessageText { get; set; }
    }
}
