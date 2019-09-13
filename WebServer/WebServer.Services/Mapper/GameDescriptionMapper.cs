using System;
using System.Collections.Generic;
using System.Text;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Mapper
{
    public class GameDescriptionMapper
    {
        public static GameDescriptionBll GetGameDescription(Game game, Offers offer, GameFinalScores scores, List<FeedbackBll> feedbacks, double UserScore)
        {
            return new GameDescriptionBll
            {
                GameID = game.GameID,
                GameName = game.GameName,
                GamePrice = offer != null ? offer.GameNewPrice : 0,
                OldGamePrice = game.GamePrice,
                AmountOfVotes = scores.AmountOfVotes,
                GameScore = scores.GameScore,
                GameImage = game.GameImage,
                GameOfferAmount = offer != null ? offer.GameOfferAmount : 0,
                GameJenre = game.GameJenre,
                GamePlatform = game.GamePlatform,
                GameRating = game.GameRating,
                Feedbacks = feedbacks,
                UserScore = UserScore
            };
        }
    }
}
