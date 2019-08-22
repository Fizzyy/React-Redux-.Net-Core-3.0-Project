using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using WebServer.DAL.Models;

namespace WebServer.Services.ModelsBll.Joins
{
    public class GameDescriptionBll
    {
        public string GameID { get; set; }

        public string GameName { get; set; }

        public decimal GamePrice { get; set; }

        public string GameJenre { get; set; }

        public double GameScore { get; set; }

        public string GameRating { get; set; }

        public int UserScore { get; set; }

        public string GamePlatform { get; set; }

        public byte[] GameImage { get; set; }

        public List<FeedbackBll> Feedbacks { get; set; }

        public GameDescriptionBll() { }

        public GameDescriptionBll(string GameID, string GameName, double GameScore, decimal GamePrice, byte[] GameImage)
        {
            this.GameID = GameID;
            this.GameName = GameName;
            this.GameScore = GameScore;
            this.GamePrice = GamePrice;
            this.GameImage = GameImage;
        }
    }
}
