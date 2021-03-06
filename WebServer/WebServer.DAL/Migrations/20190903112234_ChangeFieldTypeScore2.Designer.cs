﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebServer.DAL.Context;

namespace WebServer.DAL.Migrations
{
    [DbContext(typeof(CommonContext))]
    [Migration("20190903112234_ChangeFieldTypeScore2")]
    partial class ChangeFieldTypeScore2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebServer.DAL.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("date");

                    b.Property<string>("GameID");

                    b.Property<string>("Username")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("GameID");

                    b.HasIndex("Username");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("WebServer.DAL.Models.Game", b =>
                {
                    b.Property<string>("GameID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameImage");

                    b.Property<string>("GameJenre");

                    b.Property<string>("GameName");

                    b.Property<string>("GamePlatform");

                    b.Property<decimal>("GamePrice");

                    b.Property<string>("GameRating");

                    b.HasKey("GameID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("WebServer.DAL.Models.GameFinalScores", b =>
                {
                    b.Property<string>("GameID");

                    b.Property<double>("GameScore");

                    b.HasKey("GameID");

                    b.ToTable("GameFinalScores");
                });

            modelBuilder.Entity("WebServer.DAL.Models.GameMark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GameID");

                    b.Property<DateTime>("GameMarkDate");

                    b.Property<double>("Score");

                    b.Property<string>("Username")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("GameID");

                    b.HasIndex("Username");

                    b.ToTable("GameMarks");
                });

            modelBuilder.Entity("WebServer.DAL.Models.MoneyKeys", b =>
                {
                    b.Property<string>("KeyCode")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Value");

                    b.HasKey("KeyCode");

                    b.ToTable("MoneyKeys");
                });

            modelBuilder.Entity("WebServer.DAL.Models.Offers", b =>
                {
                    b.Property<string>("GameID");

                    b.Property<decimal>("GameNewPrice");

                    b.Property<int>("GameOfferAmount");

                    b.HasKey("GameID");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("WebServer.DAL.Models.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<string>("GameID");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("date");

                    b.Property<decimal>("TotalSum");

                    b.Property<string>("Username")
                        .HasMaxLength(30);

                    b.Property<bool>("isOrderPaid");

                    b.HasKey("Id");

                    b.HasIndex("GameID");

                    b.HasIndex("Username");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebServer.DAL.Models.RefreshTokens", b =>
                {
                    b.Property<string>("Username");

                    b.Property<string>("RefreshToken");

                    b.HasKey("Username");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("WebServer.DAL.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Role");

                    b.Property<decimal>("UserBalance");

                    b.Property<bool>("isUserBanned");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebServer.DAL.Models.Feedback", b =>
                {
                    b.HasOne("WebServer.DAL.Models.Game", "Game")
                        .WithMany("Feedbacks")
                        .HasForeignKey("GameID");

                    b.HasOne("WebServer.DAL.Models.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("WebServer.DAL.Models.GameFinalScores", b =>
                {
                    b.HasOne("WebServer.DAL.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebServer.DAL.Models.GameMark", b =>
                {
                    b.HasOne("WebServer.DAL.Models.Game", "Game")
                        .WithMany("GameMarks")
                        .HasForeignKey("GameID");

                    b.HasOne("WebServer.DAL.Models.User", "User")
                        .WithMany("GameMarks")
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("WebServer.DAL.Models.Offers", b =>
                {
                    b.HasOne("WebServer.DAL.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebServer.DAL.Models.Orders", b =>
                {
                    b.HasOne("WebServer.DAL.Models.Game", "Game")
                        .WithMany("Orders")
                        .HasForeignKey("GameID");

                    b.HasOne("WebServer.DAL.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("WebServer.DAL.Models.RefreshTokens", b =>
                {
                    b.HasOne("WebServer.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
