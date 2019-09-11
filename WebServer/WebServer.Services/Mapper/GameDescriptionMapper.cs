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
        public static GameDescriptionBll GetGameDescription(Game game, Offers offer, GameFinalScores scores, List<FeedbackBll> feedbacks)
        {
            GameDescriptionBll gameDesc = new GameDescriptionBll
            {
                GameID = game.GameID,
                GameName = game.GameName,
                OldGamePrice = game.GamePrice,
                NewGamePrice = offer.GameNewPrice,
                AmountOfVotes = scores.AmountOfVotes,
                GameImage = game.GameImage,
                GameOfferAmount = offer.GameOfferAmount,
                GameJenre = game.GameJenre,
                GamePlatform = game.GamePlatform,
                GameRating = game.GameRating,
                Feedbacks = feedbacks
            };
            return gameDesc;
        }
    }
}
