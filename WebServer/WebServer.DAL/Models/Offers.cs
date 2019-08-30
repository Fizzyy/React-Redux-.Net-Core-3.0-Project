using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class Offers
    {
        [ForeignKey("Game")]
        [Key]
        public string GameID { get; set; }

        public int GameOfferAmount { get; set; }

        public decimal GameNewPrice { get; set; }

        public Game Game { get; set; }
    }
}
