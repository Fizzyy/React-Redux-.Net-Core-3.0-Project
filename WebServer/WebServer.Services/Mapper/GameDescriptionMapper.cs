﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                GamePrice = offer != null ? offer.GameNewPrice : game.GamePrice,
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

        public static List<GameDescriptionBll> OrderGamesBy(string Type, List<GameDescriptionBll> games)
        {
            switch(Type)
            {
                case "NameAsc": games = games.OrderBy(x => x.GameName).ToList(); break;
                case "NameDesc": games = games.OrderByDescending(x => x.GameName).ToList(); break;
                case "PriceAsc": games = games.OrderBy(x => x.GamePrice).ToList(); break;
                case "PriceDesc": games = games.OrderByDescending(x => x.GamePrice).ToList(); break;
                case "ScoreAsc": games = games.OrderBy(x => x.GameScore).ToList(); break;
                case "ScoreDesc": games = games.OrderByDescending(x => x.GameScore).ToList(); break;
            }
            return games;
        }
    }
}
