﻿// <auto-generated />
using System;
using DbProvider.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(CurrencyBattleDbContext))]
    [Migration("20220523123439_m8")]
    partial class M8
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SharedModels.Models.RatingEntities.TotalPlayed", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<int?>("Games")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Login");

                    b.ToTable("t_total_pLayed_rating", (string)null);
                });

            modelBuilder.Entity("SharedModels.Models.RatingEntities.TotalVictories", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<int?>("Games")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Login");

                    b.ToTable("t_total_victories_rating", (string)null);
                });

            modelBuilder.Entity("SharedModels.Models.RatingEntities.Winrate", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<int?>("Games")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Login");

                    b.ToTable("t_winrate_rating", (string)null);
                });

            modelBuilder.Entity("SharedModels.Models.Room", b =>
                {
                    b.Property<string>("RoomId")
                        .HasColumnType("text");

                    b.Property<string>("Bets")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RoomId");

                    b.ToTable("t_rooms", (string)null);
                });

            modelBuilder.Entity("SharedModels.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(100m);

                    b.Property<string>("History")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalGames")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("Victories")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.HasKey("Login");

                    b.ToTable("t_users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
