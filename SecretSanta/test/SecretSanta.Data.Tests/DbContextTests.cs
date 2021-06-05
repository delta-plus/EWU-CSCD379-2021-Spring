using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class DbContextTests
    {
        [TestMethod]
        public async Task Add_NewUser_Succeeds()
        {
            using DbContext dbContext = new DbContext();
            dbContext.Database.Migrate();
            string prefix = $"{nameof(DbContextTests)}.{nameof(Add_NewUser_Succeeds)}";

            async Task RemoveExistingTestEventsAsync() 
            {
                IQueryable<User>? users = dbContext.Users.Where(
                    item => item.FirstName.StartsWith(prefix));

                dbContext.Users.RemoveRange(users);
                await dbContext.SaveChangesAsync();
            }

            try 
            {
//                await RemoveExistingTestEventsAsync();

                int countBefore = dbContext.Users.Count();

                User newUser = new User() { 
                    FirstName = prefix + Guid.NewGuid().ToString(), 
                    LastName = Guid.NewGuid().ToString() 
                };

//                await dbContext.Users.AddAsync(newUser);
//                await dbContext.SaveChangesAsync();








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

await dbContext.AddRangeAsync(users);
await dbContext.AddRangeAsync(groups);

await dbContext.SaveChangesAsync();





                string blah = "";
                foreach (User user in dbContext.Groups.Find(1).Users) {
                  blah += user.FirstName + " " + user.LastName + ";";
                }

                Assert.AreEqual(blah, "33");

                Assert.AreEqual(countBefore + 1, dbContext.Users.Count());
            } 
            finally 
            {
//                 await RemoveExistingTestEventsAsync();               
            }
        }
    }
}
