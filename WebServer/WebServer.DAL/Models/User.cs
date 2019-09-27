using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebServer.DAL.Models
{
    public class User
    {
        public User()
        {
            Orders = new List<Orders>();
            Feedbacks = new List<Feedback>();
            GameMarks = new List<GameMark>();
            Messages = new List<Messages>();
            ChatParticipants = new List<Participants>();
        }

        [Key]
        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public decimal UserBalance { get; set; }

        public string Role { get; set; }

        public string UserImage { get; set; }

        public List<Orders> Orders { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<GameMark> GameMarks { get; set; }
        public List<Messages> Messages { get; set; }
        public List<Participants> ChatParticipants { get; set; }
    }
}
