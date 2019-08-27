using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebServer.DAL.Models;

namespace WebServer.Services.ModelsBll
{
    public class FeedbackBll
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string GameID { get; set; }

        public string Comment { get; set; }

        public DateTime CommentDate { get; set; }

        public FeedbackBll() { }

        public FeedbackBll(int Id, string Username, string GameID, string Comment, DateTime CommentDate)
        {
            this.Id = Id;
            this.Username = Username;
            this.GameID = GameID;
            this.Comment = Comment;
            this.CommentDate = CommentDate;
        }
    }
}
