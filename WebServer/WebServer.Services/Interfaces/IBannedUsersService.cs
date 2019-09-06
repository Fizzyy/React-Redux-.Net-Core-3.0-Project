using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.Services.ModelsBll;

namespace WebServer.Services.Interfaces
{
    public interface IBannedUsersService
    {
        Task GrantBan(BannedUsersBll bannedUser);

        Task RevokeBan(string Username);

        Task UpdateBanInfo(BannedUsersBll bannedUser);
    }
}
