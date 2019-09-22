using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebServer.DAL.Models
{
    public class Game
    {
        public Game()
        {
            Orders = new List<Orders>();
            Feedbacks = new List<Feedback>();
            GameMarks = new List<GameMark>();
        }

        [Key]
        public string GameID { get; set; }

        public string GameName { get; set; }

        public decimal GamePrice { get; set; }

        public string GameJenre { get; set; }

        public string GameRating { get; set; }

        public string GamePlatform { get; set; }

        public string GameImage { get; set; }

        //public bool isActive { get; set; }

        public List<Orders> Orders { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<GameMark> GameMarks { get; set; }
    }
}
