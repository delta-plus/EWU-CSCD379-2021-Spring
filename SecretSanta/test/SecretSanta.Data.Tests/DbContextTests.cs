using System;
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

                string blah = "";
                foreach (var user in dbContext.Users) {
                  blah += user.FirstName + " ";
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
