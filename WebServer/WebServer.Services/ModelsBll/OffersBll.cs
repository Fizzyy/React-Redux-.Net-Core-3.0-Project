using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll
{
    public class OffersBll
    {
        public string GameID { get; set; }

        public double GameOfferAmount { get; set; }

        public decimal GameNewPrice { get; set; }

        public DateTime EndOfOffer { get; set; }
    }
}
