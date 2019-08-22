using System;
using System.Collections.Generic;
using System.Text;
using WebServer.DAL.Models;

namespace WebServer.Services.ModelsBll
{
    public class GameMarkBll
    {
        public string Username { get; set; }
        public string GameID { get; set; }
        public int Score { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
    }
}
