﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.DAL.Models;
using WebServer.DAL.Repository.Classes;

namespace WebServer.DAL.Repository.Interfaces
{
    public interface IGameFinalScoreRepository
    {
        Task<IEnumerable<GameFinalScores>> GetTopFinalScores3();

        Task<GameFinalScores> GetGame(string GameID);

        Task<GameFinalScores> UpdateScore(GameFinalScores game);
    }
}
