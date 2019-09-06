using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IOffersRepository
    {
        Task AddGameOffer(Offers offer);

        Task UpdateOffer(Offers offer);

        Task DeleteOffer(string OfferID);

        Task<Offers> GetOffer(string OfferID);
    }
}
