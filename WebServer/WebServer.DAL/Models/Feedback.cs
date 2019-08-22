using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebServer.DAL.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }
        public string GameID { get; set; }

        [MaxLength]
        public string Comment { get; set; }

        [Column(TypeName = "date")]
        public DateTime CommentDate { get; set; }

        public User User { get; set; }
        public Game Game { get; set; }
    }
}
