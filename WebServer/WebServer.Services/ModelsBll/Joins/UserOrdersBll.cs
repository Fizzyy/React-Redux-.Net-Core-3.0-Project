using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll.Joins
{
    public class UserOrdersBll
    {
        public int OrderID { get; set; }

        public string Username { get; set; }

        public string GameName { get; set; }

        public int Amount { get; set; }

        public DateTime OrderDate { get; set; }

        public string GamePlatform { get; set; }

        public decimal TotalSum { get; set; }
    }
}
