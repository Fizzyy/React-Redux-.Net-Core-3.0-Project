using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll
{
    public class GameBll 
    {
        public string GameID { get; set; }

        public string GameName { get; set; }

        public decimal GamePrice { get; set; }

        public string GameJenre { get; set; }

        public string GameRating { get; set; }

        public string GamePlatform { get; set; }

        public byte[] GameImage { get; set; }
    }
}
