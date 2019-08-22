using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class Orders
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        public string GameID { get; set; }

        public int Amount { get; set; }

        public decimal TotalSum { get; set; }

        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }
        public bool isOrderPaid { get; set; }

        public User User { get; set; }
        public Game Game { get; set; }
    }
}
