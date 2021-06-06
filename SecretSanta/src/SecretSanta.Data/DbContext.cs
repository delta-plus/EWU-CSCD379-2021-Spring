using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbContext = SecretSanta.Data.DbContext;

namespace SecretSanta.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext() : base(new DbContextOptionsBuilder<DbContext>().UseSqlite("Data Source=main.db").Options)
        {
            Database.Migrate();
        }

        public Microsoft.EntityFrameworkCore.DbSet<User> Users => Set<User>();
        public Microsoft.EntityFrameworkCore.DbSet<Group> Groups => Set<Group>();
        public Microsoft.EntityFrameworkCore.DbSet<Gift> Gifts => Set<Gift>();
        public Microsoft.EntityFrameworkCore.DbSet<Assignment> Assignments => Set<Assignment>();
//        public Microsoft.EntityFrameworkCore.DbSet<GroupUser> GroupUsers => Set<GroupUser>();
//        public Microsoft.EntityFrameworkCore.DbSet<GroupAssignment> GroupAssignments => Set<GroupAssignment>();

        private StreamWriter LogStream { get; } = new StreamWriter("db.log", append: true);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            optionsBuilder.LogTo(LogStream.WriteLine);
        }

        public override void Dispose()
        {
            base.Dispose();
            LogStream.Dispose();
            GC.SuppressFinalize(this);
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await LogStream.DisposeAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<User>().HasAlternateKey(user => new { user.FirstName, user.LastName });
            modelBuilder.Entity<Group>().HasAlternateKey(group => new { group.Name });
            modelBuilder.Entity<Gift>().HasAlternateKey(gift => new { gift.Title });
            modelBuilder.Entity<Assignment>().HasAlternateKey(assignment => new { assignment.GiverName });
//            modelBuilder.Entity<GroupUser>().HasKey(item => new { item.GroupId, item.UserId });

            // Just got to help EntityFramework figure out what it's looking at.
            modelBuilder.Entity<Assignment>()
                .Property(x => x.Giver)
                .HasConversion(
                    y => JsonSerializer.Serialize(y, null),
                    y => JsonSerializer.Deserialize<User>(y, null)
                );

            modelBuilder.Entity<Assignment>()
                .Property(x => x.Receiver)
                .HasConversion(
                    y => JsonSerializer.Serialize(y, null),
                    y => JsonSerializer.Deserialize<User>(y, null)
                );

/*
            // Seed data goes here.
            var users = new[]
            {
                new User
                {
                    Id = 1,
                    FirstName = "Inigov",
                    LastName = "Montoyav"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Princess",
                    LastName = "Buttercup"
                },
                new User
                {
                    Id = 3,
                    FirstName = "Prince",
                    LastName = "Humperdink"
                },
                new User
                {
                    Id = 4,
                    FirstName = "Count",
                    LastName = "Rugen"
                },
                new User
                {
                    Id = 5,
                    FirstName = "Miracle",
                    LastName = "Max"
                }
            };

            var groups = new[]
            {
                new Group
                {
                    Id = 1,
                    Name = "IntelliTect Christmas Party",
                },
                new Group
                {
                    Id = 2,
                    Name = "Friends",
                }
            };

            var groupUsers = new[]
            {
              new GroupUser
              {
                  Id = 1,
                  GroupId = 1,
                  UserId = 1
              },
              new GroupUser
              {
                  Id = 2,
                  GroupId = 1,
                  UserId = 2
              },
              new GroupUser
              {
                  Id = 3,
                  GroupId = 1,
                  UserId = 3
              },
              new GroupUser
              {
                  Id = 4,
                  GroupId = 1,
                  UserId = 4
              },
              new GroupUser
              {
                  Id = 5,
                  GroupId = 1,
                  UserId = 5
              },
              new GroupUser
              {
                  Id = 6,
                  GroupId = 2,
                  UserId = 1
              },
              new GroupUser
              {
                  Id = 7,
                  GroupId = 2,
                  UserId = 2
              },
              new GroupUser
              {
                  Id = 8,
                  GroupId = 2,
                  UserId = 3
              },
              new GroupUser
              {
                  Id = 9,
                  GroupId = 2,
                  UserId = 4
              },
              new GroupUser
              {
                  Id = 10,
                  GroupId = 2,
                  UserId = 5
              },
            };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<GroupUser>().HasData(groupUsers); */
        }
    }
}
