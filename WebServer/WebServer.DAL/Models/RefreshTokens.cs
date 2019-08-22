using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class RefreshTokens
    {
        [Key]
        [ForeignKey("User")]
        public string Username { get; set; }

        public string RefreshToken { get; set; }

        public User User { get; set; }
    }
}
