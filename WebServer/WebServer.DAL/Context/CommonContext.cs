using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebServer.DAL.Models;

namespace WebServer.DAL.Context
{
    public class CommonContext : DbContext
    {
        public CommonContext(DbContextOptions options) : base(options) { }
        public CommonContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<BannedUsers> BannedUsers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<GameMark> GameMarks { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GamesScreenshots> GamesScreenshots { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<MoneyKeys> MoneyKeys { get; set; }
        public DbSet<GameFinalScores> GameFinalScores { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>().HasMany(x => x.Orders).WithOne(x => x.User);
            model.Entity<User>().HasMany(x => x.Feedbacks).WithOne(x => x.User);
            model.Entity<User>().HasMany(x => x.GameMarks).WithOne(x => x.User);

            model.Entity<Game>().HasMany(x => x.Orders).WithOne(x => x.Game);
            model.Entity<Game>().HasMany(x => x.Feedbacks).WithOne(x => x.Game);
            model.Entity<Game>().HasMany(x => x.GameMarks).WithOne(x => x.Game);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Kursach;Trusted_Connection=True;");
        }
    }
}
