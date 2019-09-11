using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.ModelsBll
{
    public class UserBll
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public decimal UserBalance { get; set; }

        public bool isUserBanned { get; set; }

        public string Role { get; set; }

        public string UserImage { get; set; }

        public List<UserOrdersBll> Orders { get; set; }
        public List<UserFeedbackBll> Feedbacks { get; set; }
        public List<UserScoresBll> GameMarks { get; set; }
    }
}
