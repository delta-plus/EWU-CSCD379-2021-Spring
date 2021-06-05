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

        }

        public Microsoft.EntityFrameworkCore.DbSet<User> Users => Set<User>();
        public Microsoft.EntityFrameworkCore.DbSet<Group> Groups => Set<Group>();
        public Microsoft.EntityFrameworkCore.DbSet<Gift> Gifts => Set<Gift>();
        public Microsoft.EntityFrameworkCore.DbSet<Assignment> Assignments => Set<Assignment>();

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
            modelBuilder.Entity<GroupUser>().HasKey(item => new { item.GroupId, item.UserId });

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

            // Seed data goes here.
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        FirstName = "Inigo",
                        LastName = "Montoya"
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
                );

            modelBuilder.Entity<Group>()
                .HasData(
                    new Group
                    {
                        Id = 1,
                        Name = "IntelliTect Christmas Party",
                        GroupUser = new List<GroupUser> 
                        {
                            new GroupUser
                            {
                                Users = new List<User>
                                {
                                    new User
                                    {
                                        Id = 1,
                                        FirstName = "Inigo",
                                        LastName = "Montoya"
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
                                }
                            }                           
                        }
                    },
                    new Group
                    {
                        Id = 2,
                        Name = "Friends",
                        GroupUser = new List<GroupUser> 
                        {
                            new GroupUser
                            {
                                Users = new List<User>
                                {
                                    new User
                                    {
                                        Id = 1,
                                        FirstName = "Inigo",
                                        LastName = "Montoya"
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
                                }
                            }
                        }
                    }
                );
        }
    }
}
