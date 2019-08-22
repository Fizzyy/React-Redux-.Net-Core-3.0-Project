using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class GameFinalScores
    {
        [Key]
        [ForeignKey("Game")]
        public string GameID { get; set; }

        public double GameScore { get; set; }

        public Game Game { get; set; }
    }
}
