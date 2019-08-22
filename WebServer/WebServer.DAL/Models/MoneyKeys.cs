using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebServer.DAL.Models
{
    public class MoneyKeys
    {
        [Key]
        public string KeyCode { get; set; }

        public decimal Value { get; set; }
    }
}
