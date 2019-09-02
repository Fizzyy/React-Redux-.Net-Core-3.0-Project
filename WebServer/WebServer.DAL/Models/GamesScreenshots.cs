using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class GamesScreenshots
    {
        [ForeignKey("Game")]
        [Key]
        public string GameID { get; set; }

        public string GameScreenshotReference1 { get; set; }

        public string GameScreenshotReference2 { get; set; }

        public string GameScreenshotReference3 { get; set; }

        public string GameDescriptionBackground { get; set; }

        public Game Game { get; set; }
    }
}
