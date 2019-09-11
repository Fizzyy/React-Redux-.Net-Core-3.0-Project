using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Services.ModelsBll.Joins
{
    public class UserFeedbackBll
    {
        public int CommentID { get;set; }

        public string Username { get; set; }

        public string GameName { get; set; }

        public string GamePlatform { get; set; }

        public string Comment { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
