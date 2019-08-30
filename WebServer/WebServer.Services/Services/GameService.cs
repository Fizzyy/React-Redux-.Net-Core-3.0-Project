using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll.Joins;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly IGameFinalScoreRepository gameFinalScoreRepository;
        private readonly IGameMarkRepository gameMarkRepository;
        private readonly IFeedbackRepository feedbackRepository;

        public GameService(IGameRepository gameRepository, IGameFinalScoreRepository gameFinalScoreRepository, IFeedbackRepository feedbackRepository, IGameMarkRepository gameMarkRepository)
        {
            this.gameRepository = gameRepository;
            this.gameFinalScoreRepository = gameFinalScoreRepository;
            this.feedbackRepository = feedbackRepository;
            this.gameMarkRepository = gameMarkRepository;
        }

        public Task<IEnumerable<Game>> GetAllGames()
        {
            return gameRepository.GetAllGames();
        }

        public async Task<List<GameDescriptionBll>> GetCurrentPlatformGames(string GamePlatform)
        {
            var games = await gameRepository.GetCurrentPlatformGames(GamePlatform);
            if (games == null) return null;

            List<GameDescriptionBll> returnedgames = new List<GameDescriptionBll>();
            foreach (var game in games)
            {
                returnedgames.Add(new GameDescriptionBll
                (
                    game.GameID, game.GameName, await gameFinalScoreRepository.GetGameScore(game.GameID), game.GamePrice, game.GameRating, game.GameJenre, game.GameImage
                ));
            }
            return returnedgames;
        }

        public async Task<List<GameDescriptionBll>> OrderGames(string GamePlatform, string Type, string TypeValue)
        {
            List<GameDescriptionBll> games = new List<GameDescriptionBll>();

            var foundgames = await gameRepository.GetCurrentPlatformGames(GamePlatform);
            if (foundgames == null) return null;

            foreach (var game in foundgames)
            {
                games.Add(new GameDescriptionBll
                (
                    game.GameID, game.GameName, await gameFinalScoreRepository.GetGameScore(game.GameID), game.GamePrice, game.GameRating, game.GameJenre, game.GameImage
                ));
            }

            switch (Type)
            {
                case "Genre":
                    {
                        if (TypeValue != "All") games = games.Where(x => x.GameJenre == TypeValue).ToList();
                        break;
                    }
                case "Rating":
                    {
                        if (TypeValue != "All") games = games.Where(x => x.GameRating == TypeValue).ToList();
                        break;
                    }
                case "NameAsc":
                    {
                        games = games.OrderBy(x => x.GameName).ToList();
                        break;
                    }
                case "NameDesc":
                    {
                        games = games.OrderByDescending(x => x.GameName).ToList();
                        break;
                    }
                case "PriceAsc":
                    {
                        games = games.OrderBy(x => x.GamePrice).ToList();
                        break;
                    }
                case "PriceDesc":
                    {
                        games = games.OrderByDescending(x => x.GamePrice).ToList();
                        break;
                    }
                case "ScoreAsc":
                    {
                        games = games.OrderBy(x => x.GameScore).ToList();
                        break;
                    }
                case "ScoreDesc":
                    {
                        games = games.OrderByDescending(x => x.GameScore).ToList();
                        break;
                    }
            }
            return games;
        }

        public async Task<GameDescriptionBll> GetChosenGame(string GameID, string Username)
        {
            var foundgame = await gameRepository.GetChosenGame(GameID);
            if (foundgame == null) return null;

            List<FeedbackBll> feedbacks = new List<FeedbackBll>();
            foreach (var feedback in await feedbackRepository.GetCurrentGameFeedback(foundgame.GameID))
            {
                feedbacks.Add(new FeedbackBll(feedback.Id, feedback.Username, feedback.GameID, feedback.Comment, feedback.CommentDate));
            }

            GameDescriptionBll gameDescription = new GameDescriptionBll
            {
                GameID = foundgame.GameID,
                GameName = foundgame.GameName,
                GameScore = await gameFinalScoreRepository.GetGameScore(foundgame.GameID),
                GameJenre = foundgame.GameJenre,
                GamePlatform = foundgame.GamePlatform,
                GamePrice = foundgame.GamePrice,
                GameRating = foundgame.GameRating,
                UserScore = await gameMarkRepository.GetCurrentUserScore(GameID, Username),
                Feedbacks = feedbacks,
                GameImage = foundgame.GameImage
            };
            return gameDescription;
        }

        public async Task AddGame(GameBll game)
        {
            await gameRepository.AddGame(new Game
            {
                GameID = game.GameID,
                GameName = game.GameName,
                GameJenre = game.GameJenre,
                GamePrice = game.GamePrice,
                GamePlatform = game.GamePlatform,
                GameRating = game.GameRating,
                GameImage = game.GameImage
            });
        }

        public async Task UpdateGame(GameBll game)
        {
            await gameRepository.UpdateGame(new Game
            {
                GameID = game.GameID,
                GameName = game.GameName,
                GameJenre = game.GameJenre,
                GamePrice = game.GamePrice,
                GamePlatform = game.GamePlatform,
                GameRating = game.GameRating,
                GameImage = game.GameImage
            });
        }

        public async Task RemoveGame(string GameID)
        {
            await gameRepository.RemoveGame(GameID);
        }
    }
}