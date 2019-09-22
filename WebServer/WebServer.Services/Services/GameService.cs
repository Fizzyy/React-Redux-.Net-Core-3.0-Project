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
using WebServer.Services.Mapper;

namespace WebServer.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly IGameFinalScoreRepository gameFinalScoreRepository;
        private readonly IGameMarkRepository gameMarkRepository;
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IUserRepository userRepository;
        private readonly IOffersRepository offersRepository;

        public GameService(IGameRepository gameRepository, IGameFinalScoreRepository gameFinalScoreRepository, IFeedbackRepository feedbackRepository, IGameMarkRepository gameMarkRepository, IUserRepository userRepository, IOffersRepository offersRepository)
        {
            this.gameRepository = gameRepository;
            this.gameFinalScoreRepository = gameFinalScoreRepository;
            this.feedbackRepository = feedbackRepository;
            this.gameMarkRepository = gameMarkRepository;
            this.userRepository = userRepository;
            this.offersRepository = offersRepository;
        }

        public async Task<List<GameDescriptionBll>> GetAllGames()
        {
            var list = new List<GameDescriptionBll>();
        
            var allgames = await gameRepository.GetAllGames();
            foreach (var game in allgames)
            {
                var TotalFeedback = new List<FeedbackBll>();

                var gamescores = await gameFinalScoreRepository.GetGame(game.GameID);
                var feedbacks = await feedbackRepository.GetCurrentGameFeedback(game.GameID);

                foreach (var feedback in feedbacks)
                {
                    TotalFeedback.Add(new FeedbackBll
                    {

                        Id = feedback.Id,
                        Comment = feedback.Comment,
                        CommentDate = feedback.CommentDate,
                        Username = feedback.Username,
                        GameID = game.GameID

                    });
                }
                
                list.Add(GameDescriptionMapper.GetGameDescription(game, null, gamescores, TotalFeedback, 0));
            }
            return list;
        }

        public async Task<List<GameDescriptionBll>> GetCurrentPlatformGames(string GamePlatform)
        {
            var games = await gameRepository.GetCurrentPlatformGames(GamePlatform);
            if (games == null) return null;

            List<GameDescriptionBll> returnedgames = new List<GameDescriptionBll>();
            foreach (var game in games)
            {
                var gamescores = await gameFinalScoreRepository.GetGame(game.GameID);
                var offer = await offersRepository.GetOffer(game.GameID);

                returnedgames.Add(GameDescriptionMapper.GetGameDescription(game, offer, gamescores, null, 0));
            }
            return returnedgames;
        }

        public async Task<List<GameDescriptionBll>> OrderGames(string GamePlatform, string Type, string Age, string Genre)
        {
            List<GameDescriptionBll> games = new List<GameDescriptionBll>();

            var foundgames = await gameRepository.GetCurrentPlatformGames(GamePlatform);
            if (foundgames == null) return null;

            foreach (var game in foundgames)
            {
                var gamescores = await gameFinalScoreRepository.GetGame(game.GameID);
                var offer = await offersRepository.GetOffer(game.GameID);

                games.Add(GameDescriptionMapper.GetGameDescription(game, offer, gamescores, null, 0));
            }

            if (Genre != "All" && Age != "All+")
            {
                games = games.Where(x => x.GameJenre == Genre && x.GameRating == Age).ToList();
                games = GameDescriptionMapper.OrderGamesBy(Type, games);
            }
            else if (Genre == "All" && Age != "All+")
            {
                games = games.Where(x => x.GameRating == Age).ToList();
                games = GameDescriptionMapper.OrderGamesBy(Type, games);
            }
            else if (Genre != "All" && Age == "All+")
            {
                games = games.Where(x => x.GameJenre == Genre).ToList();
                games = GameDescriptionMapper.OrderGamesBy(Type, games);
            }
            else if (Genre == "All" && Age == "All+")
            {
                games = GameDescriptionMapper.OrderGamesBy(Type, games);
            }
            return games;
        }

        public async Task<List<GameDescriptionBll>> GetSameJenreGames(string GameGenre, string GameID)
        {
            var list = new List<GameDescriptionBll>();
            var games = await gameRepository.GetSameJenreGames(GameGenre, GameID);
            foreach (var game in games)
            {
                var score = await gameFinalScoreRepository.GetGame(game.GameID);

                list.Add(GameDescriptionMapper.GetGameDescription(game, null, score, null, 0));
            }
            return list;
        }

        public async Task<List<GameDescriptionBll>> GetGamesByRegex(string GamePlatform, string GameName)
        {
            var games = await gameRepository.GetGamesByRegex(GamePlatform, GameName);
            if (games == null) return null;

            List<GameDescriptionBll> returnedgames = new List<GameDescriptionBll>();
            foreach (var game in games)
            {
                var gamescores = await gameFinalScoreRepository.GetGame(game.GameID);
                var offer = await offersRepository.GetOffer(game.GameID);

                returnedgames.Add(GameDescriptionMapper.GetGameDescription(game, offer, gamescores, null, 0));
            }
            return returnedgames;
        }

        public async Task<GameDescriptionBll> GetChosenGame(string GameID, string Username)
        {
            var foundgame = await gameRepository.GetChosenGame(GameID);
            if (foundgame == null) return null;

            var gamescores = await gameFinalScoreRepository.GetGame(GameID);
            var offer = await offersRepository.GetOffer(GameID);
            var userScore = await gameMarkRepository.GetCurrentUserScore(GameID, Username);

            List<FeedbackBll> feedbacks = new List<FeedbackBll>();
            foreach (var feedback in await feedbackRepository.GetCurrentGameFeedback(foundgame.GameID))
            {
                var user = await userRepository.GetCurrentUser(feedback.Username);
                feedbacks.Add(new FeedbackBll(feedback.Id, feedback.Username, feedback.GameID, feedback.Comment, feedback.CommentDate, user.UserImage));
            }

            return GameDescriptionMapper.GetGameDescription(foundgame, offer, gamescores, feedbacks, userScore);
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

        public async Task<object> GetGamesForStartPage()
        {
            var TopOfferRatedGames = await offersRepository.GetTop3();
            var TopVotedGames = await gameFinalScoreRepository.GetTopFinalScores3();

            var RatedGames = new List<GameDescriptionBll>();
            var VotedGames = new List<GameDescriptionBll>();

            foreach (var game in TopOfferRatedGames)
            {
                var gamedesc = await gameRepository.GetChosenGame(game.GameID);
                var gamescores = await gameFinalScoreRepository.GetGame(game.GameID);

                RatedGames.Add(GameDescriptionMapper.GetGameDescription(gamedesc, game, gamescores, null, 0));
            }


            foreach (var game in TopVotedGames)
            {
                var gamedesc = await gameRepository.GetChosenGame(game.GameID);
                var gameoffer = await offersRepository.GetOffer(game.GameID);

                VotedGames.Add(GameDescriptionMapper.GetGameDescription(gamedesc, gameoffer, game, null, 0));
            }

            return new
            {
                Rated = RatedGames,
                Offers = VotedGames
            };
        }
    }
}