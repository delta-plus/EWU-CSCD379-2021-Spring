using System.Linq;
using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class DataSeeder
    {
        public static void Seed()
        {
            using DbContext dbContext = new DbContext();

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
    }
}
