﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Interfaces
{
    public interface IOffersService
    {
        Task AddGameOffer(OffersBll offer);

        Task UpdateOffer(OffersBll offer);

        Task DeleteOffer(string OfferID);

        Task<Offers> GetOffer(string OfferID);
    }
}