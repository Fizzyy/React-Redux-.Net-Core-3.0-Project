using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace WebServer.DAL.Repository.Classes
{
    public class GameRepository : IGameRepository
    {
        private readonly CommonContext commonContext;

        public GameRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await Task.FromResult(commonContext.Games);
        }

        public async Task<IEnumerable<Game>> GetCurrentPlatformGames(string GamePlatform)
        {
            if (GamePlatform == "All") return await Task.FromResult(commonContext.Games);
            return await Task.FromResult(commonContext.Games.Where(x => x.GamePlatform == GamePlatform));
        }

        public async Task<Game> GetChosenGame(string GameID)
        {
            var chosengame = await commonContext.Games.FindAsync(GameID);
            if (chosengame != null) return chosengame;
            else return null;
        }

        public async Task<IEnumerable<Game>> GetSameJenreGames(string GameGenre, string GameID)
        {
            return await Task.FromResult(commonContext.Games.Where(x => x.GameJenre == GameGenre && x.GameID != GameID).Take(5));
        }

        public async Task<List<Game>> GetGamesByRegex(string GamePlatform, string GameName)
        {
            List<Game> games = new List<Game>();
            List<Game> platformgames = new List<Game>();

            if (GamePlatform != "All") platformgames = await commonContext.Games.Where(x => x.GamePlatform == GamePlatform).ToListAsync();
            else platformgames = await commonContext.Games.ToListAsync();

            if (GameName == null || GameName == "") return platformgames;

            Regex regex = new Regex($@"^{GameName}\w*", RegexOptions.IgnoreCase);
            foreach (var game in platformgames)
            {
                Match match = regex.Match(game.GameName);
                if (match.Success) games.Add(game); 
            }
            return games;
        }

        public async Task AddGame(Game game)
        {
            commonContext.Games.Add(game);
            commonContext.GameFinalScores.Add(new GameFinalScores { GameID = game.GameID, GameScore = 0 });
            await commonContext.SaveChangesAsync();
        }

        public async Task UpdateGame(Game game)
        {
            var foundgame = await commonContext.Games.FindAsync(game.GameID);
            if (foundgame != null)
            {
                foundgame.GameName = game.GameName;
                foundgame.GamePlatform = game.GamePlatform;
                foundgame.GamePrice = game.GamePrice;
                foundgame.GameRating = game.GameRating;
                foundgame.GameJenre = game.GameJenre;
                foundgame.GameImage = game.GameImage;
            }
            await commonContext.SaveChangesAsync();
        }

        public async Task RemoveGame(string GameID)
        {
            var game = await commonContext.Games.FindAsync(GameID);
            if (game != null) commonContext.Remove(game);
            await commonContext.SaveChangesAsync();
        }
    }
}