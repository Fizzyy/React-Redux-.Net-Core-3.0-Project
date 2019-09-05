using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll.Joins
{
    public class UserScoresBll
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string GameName { get; set; }

        public string GamePlatform { get; set; }

        public double Score { get; set; }

        public int ScoreID { get; set; }
    }
}
