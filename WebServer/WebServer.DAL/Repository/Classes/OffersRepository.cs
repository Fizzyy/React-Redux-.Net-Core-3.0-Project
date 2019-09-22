using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Context;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Interfaces;

namespace WebServer.DAL.Repository.Classes
{
    public class OffersRepository : IOffersRepository
    {
        private readonly CommonContext commonContext;

        public OffersRepository(CommonContext commonContext)
        {
            this.commonContext = commonContext;
        }

        public async Task<IEnumerable<Offers>> GetAllOffers()
        {
            return await Task.FromResult(commonContext.Offers);
        }

        public async Task AddGameOffer(Offers offer)
        {
            var DefaultGame = await commonContext.Games.FindAsync(offer.GameID);
            decimal DefaultGamePrice = DefaultGame.GamePrice;
            offer.GameNewPrice = DefaultGamePrice - (DefaultGamePrice * Convert.ToDecimal(offer.GameOfferAmount / 100));
            await commonContext.Offers.AddAsync(offer);
            await commonContext.SaveChangesAsync();
        }

        public async Task DeleteOffer(string OfferID)
        {
            var offer = await commonContext.Offers.FindAsync(OfferID);
            if (offer != null) commonContext.Offers.Remove(offer);
            await commonContext.SaveChangesAsync();
        }

        public async Task<Offers> GetOffer(string OfferID)
        {
            var offer = await commonContext.Offers.FirstOrDefaultAsync(x => x.GameID == OfferID);
            if (offer != null) return offer;
            return null;
        }

        public async Task UpdateOffer(Offers offer)
        {
            var FoundOffer = await commonContext.Offers.FindAsync(offer.GameID);
            var DefaultGame = await commonContext.Games.FindAsync(offer.GameID);
            decimal DefaultGamePrice = DefaultGame.GamePrice;

            decimal tempCost = DefaultGamePrice - (DefaultGamePrice * Convert.ToDecimal(offer.GameOfferAmount / 100));

            if (offer != null)
            {
                FoundOffer.GameOfferAmount = offer.GameOfferAmount;
                FoundOffer.GameNewPrice = tempCost;
            }
            await commonContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Offers>> GetTop3()
        {
            return await Task.FromResult(commonContext.Offers.OrderBy(x => x.GameNewPrice).Take(3));
        }
    }
}
