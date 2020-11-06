﻿// <auto-generated />
using System;
using Game2v;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Game2v.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201102202331_InitDB")]
    partial class InitDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Game2v.Model.Friend", b =>
                {
                    b.Property<int>("FriendId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FriendName")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("FriendId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("Game2v.Model.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FriendId");

                    b.Property<string>("GameTitle")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("GameId");

                    b.HasIndex("FriendId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Game2v.Model.Game", b =>
                {
                    b.HasOne("Game2v.Model.Friend", "Friend")
                        .WithMany("Games")
                        .HasForeignKey("FriendId");
                });
#pragma warning restore 612, 618
        }
    }
}
