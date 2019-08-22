using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll
{
    public class OrdersBll
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string GameID { get; set; }
        
        public int Amount { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal TotalSum { get; set; }

        public UserBll User { get; set; }
        public GameBll Game { get; set; }
    }
}
