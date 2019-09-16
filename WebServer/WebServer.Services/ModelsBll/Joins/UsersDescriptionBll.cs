using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll.Joins
{
    public class UsersDescriptionBll
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public decimal UserBalance { get; set; }

        public string Role { get; set; }

        public string UserImage { get; set; }

        public bool isUserBanned { get; set; }

        public string BanReason { get; set; }

        public DateTime BanDate { get; set; }

        public List<UserOrdersBll> Orders { get; set; }
        public List<UserFeedbackBll> Feedbacks { get; set; }
        public List<UserScoresBll> GameMarks { get; set; }
    }
}
