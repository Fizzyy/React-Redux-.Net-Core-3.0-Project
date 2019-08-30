﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.Services.ModelsBll;
using WebServer.Services.ModelsBll.Joins;

namespace WebServer.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGames();

        Task<List<GameDescriptionBll>> GetCurrentPlatformGames(string GamePlatform);

        Task<GameDescriptionBll> GetChosenGame(string GameID, string Username);

        Task<List<GameDescriptionBll>> OrderGames(string GamePlatform, string Type, string TypeValue);

        Task AddGame(GameBll game);

        Task UpdateGame(GameBll game);

        Task RemoveGame(string GameID);
    }
}
