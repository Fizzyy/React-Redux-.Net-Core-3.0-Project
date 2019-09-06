using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;
using WebServer.Services.Interfaces;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Services
{
    public class OffersService : IOffersService
    {
        private readonly IOffersRepository offersRepository;

        public OffersService(IOffersRepository offersRepository)
        {
            this.offersRepository = offersRepository;
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
