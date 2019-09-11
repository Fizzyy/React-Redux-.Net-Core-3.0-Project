using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Services
{
    public class OffersService : IOffersService
    {
        private readonly IOffersRepository offersRepository;

        private readonly IGameService gameService;

        public OffersService(IOffersRepository offersRepository, IGameService gameService)
        {
            this.offersRepository = offersRepository;
            this.gameService = gameService;
        }

        public async Task<List<GameDescriptionBll>> GetAllOfferGames()
        {
            var offers = await offersRepository.GetAllOffers();

            var games = new List<GameDescriptionBll>();
            foreach (var offer in offers)
            {
                var game = await gameService.GetChosenGame(offer.GameID, null);

                games.Add(new GameDescriptionBll
                {
                    GameID = game.GameID,
                    GameName = game.GameName,
                    OldGamePrice = game.GamePrice,
                    NewGamePrice = offer.GameNewPrice,
                    AmountOfVotes = game.AmountOfVotes,
                    GameImage = game.GameImage,
                    GameOfferAmount = offer.GameOfferAmount,
                    GameJenre = game.GameJenre,
                    GamePlatform = game.GamePlatform,
                    GameRating = game.GameRating
                });
            }

            return games;
        }

        public async Task AddGameOffer(OffersBll offer)
        {
            await offersRepository.AddGameOffer(new Offers
            {
                GameID = offer.GameID,
                GameOfferAmount = offer.GameOfferAmount,
                EndOfOffer = offer.EndOfOffer
            });
        }

        public async Task DeleteOffer(string OfferID)
        {
            await offersRepository.DeleteOffer(OfferID);
        }

        public async Task<Offers> GetOffer(string OfferID)
        {
            return await offersRepository.GetOffer(OfferID);
        }

        public async Task UpdateOffer(OffersBll offer)
        {
            await offersRepository.UpdateOffer(new Offers
            {
                GameID = offer.GameID,
                GameOfferAmount = offer.GameOfferAmount,
                GameNewPrice = offer.GameNewPrice
            });
        }
    }
}
