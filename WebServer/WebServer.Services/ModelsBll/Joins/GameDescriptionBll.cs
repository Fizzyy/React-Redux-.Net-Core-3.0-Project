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

        public double GameScore { get; set; }

        public string GameJenre { get; set; }

        public string GameRating { get; set; }

        public double UserScore { get; set; }

        public string GamePlatform { get; set; }

        public string GameImage { get; set; }

        public int AmountOfVotes { get; set; }

        public decimal GamePrice { get; set; }

        public decimal OldGamePrice { get; set; }

        public double GameOfferAmount { get; set; }

        public List<FeedbackBll> Feedbacks { get; set; }

        public GameDescriptionBll() { }

        public GameDescriptionBll(string GameID, string GameName, double GameScore, decimal GamePrice, string GameRating, string GameJenre, string GameImage)
        {
            this.GameID = GameID;
            this.GameName = GameName;
            this.GameScore = GameScore;
            this.GamePrice = GamePrice;
            this.GameImage = GameImage;
            this.GameRating = GameRating;
            this.GameJenre = GameJenre;
        }
    }
}
