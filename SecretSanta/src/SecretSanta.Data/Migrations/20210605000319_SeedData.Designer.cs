﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecretSanta.Data;

namespace SecretSanta.Data.Migrations
{
    [DbContext(typeof(DbContext))]
    [Migration("20210605000319_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("SecretSanta.Data.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Giver")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GiverName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Receiver")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("GiverName");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("SecretSanta.Data.Gift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasAlternateKey("Title");

                    b.ToTable("Gifts");
                });

            modelBuilder.Entity("SecretSanta.Data.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasIndex("UserId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "IntelliTect Christmas Party"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Friends"
                        });
                });

            modelBuilder.Entity("SecretSanta.Data.GroupUser", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GroupId", "UserId");

                    b.ToTable("GroupUser");
                });

            modelBuilder.Entity("SecretSanta.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("FirstName", "LastName");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Inigo",
                            LastName = "Montoya"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Princess",
                            LastName = "Buttercup"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Prince",
                            LastName = "Humperdink"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Count",
                            LastName = "Rugen"
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Miracle",
                            LastName = "Max"
                        });
                });

            modelBuilder.Entity("SecretSanta.Data.Group", b =>
                {
                    b.HasOne("SecretSanta.Data.User", null)
                        .WithMany("Groups")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SecretSanta.Data.User", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
