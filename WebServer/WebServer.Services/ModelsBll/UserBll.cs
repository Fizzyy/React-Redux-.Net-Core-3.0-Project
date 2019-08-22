using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll
{
    public class UserBll
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public decimal UserBalance { get; set; }

        public bool isUserBanned { get; set; }

        public string Role { get; set; }

        public List<OrdersBll> Orders { get; set; }
        public List<FeedbackBll> Feedbacks { get; set; }
        public List<GameMarkBll> GameMarks { get; set; }
    }
}
