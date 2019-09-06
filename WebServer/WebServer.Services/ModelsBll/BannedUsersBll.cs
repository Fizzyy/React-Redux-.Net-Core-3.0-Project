using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll
{
    public class BannedUsersBll
    {
        public string Username { get; set; }

        public string BanReason { get; set; }

        public DateTime BanDate { get; set; }
    }
}
