using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;

namespace SecretSanta.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            using SecretSanta.Data.DbContext dbContext = new SecretSanta.Data.DbContext();
            dbContext.Database.Migrate();

            // Seed data goes here.
            var users = new[]
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
            };
            
            var groups = new[]
            {
                new Group
                {
                    Id = 1,
                    Name = "IntelliTect Christmas Party",
                    Users = new List<User> { users[0], users[1], users[2], users[3], users[4] }
                },
                new Group
                {
                    Id = 2,
                    Name = "Friends",
                    Users = new List<User> { users[0], users[1], users[2], users[3], users[4] }
                }
            };

            dbContext.AddRange(users);
            dbContext.AddRange(groups);

            dbContext.SaveChanges();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
