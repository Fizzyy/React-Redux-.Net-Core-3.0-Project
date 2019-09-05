using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class BannedUsers
    {
        [ForeignKey("User")]
        [Key]
        public string Username { get; set; }

        public string BanReason { get; set; }

        public DateTime BanDate { get; set; }

        public User User { get; set; }
    }
}
