using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebServer.DAL.Models
{
    public class GameMark
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        public string GameID { get; set; }

        public DateTime GameMarkDate { get; set; }

        public int Score { get; set; }

        public User User { get; set; }

        public Game Game { get; set; }
    }
}
