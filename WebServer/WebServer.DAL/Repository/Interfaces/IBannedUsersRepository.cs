using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IBannedUsersRepository
    {
        Task GrantBan(BannedUsers bannedUser);

        Task RevokeBan(string Username);

        Task UpdateBanInfo(BannedUsers bannedUser);

        Task<BannedUsers> FindBannedUser(string Username);
    }
}
