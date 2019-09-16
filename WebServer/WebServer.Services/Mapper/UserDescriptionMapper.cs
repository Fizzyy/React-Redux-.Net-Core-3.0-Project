using System;
using System.Collections.Generic;
using System.Text;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Mapper
{
    public class UserDescriptionMapper
    {
        public static UsersDescriptionBll GetUser(User user, BannedUsers banned, List<UserFeedbackBll> feedbacks, List<UserScoresBll> scores, List<UserOrdersBll> orders)
        {
            return new UsersDescriptionBll
            {
                Username = user.Username,
                Password = user.Password,
                UserBalance = user.UserBalance,
                UserImage = user.UserImage,
                Role = user.Role,
                BanReason = banned != null ? banned.BanReason : "-",
                BanDate = banned != null ? banned.BanDate : DateTime.MinValue,

                Feedbacks = feedbacks,
                GameMarks = scores,
                Orders = orders
            };
        }
    }
}
