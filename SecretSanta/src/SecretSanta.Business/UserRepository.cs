using System.Linq;
using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class UserRepository : IUserRepository
    {
        public User Create(User item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            using DbContext dbContext = new DbContext();

            dbContext.Users.Add(item);

            dbContext.SaveChanges();

            return item;
        }

        public User CreateGift(Gift item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            using DbContext dbContext = new DbContext();

            dbContext.Gifts.Add(item);

            dbContext.SaveChanges();

            return item;
        }

        public User? GetItem(int id)
        {
            using DbContext dbContext = new DbContext();

            return dbContext.Users.Find(id);
        }

        public ICollection<User> List()
        {
            using DbContext dbContext = new DbContext();

            return dbContext.Users.ToList();
        }

        public bool Remove(int id)
        {
            using DbContext dbContext = new DbContext();

            User user = dbContext.Users.Find(id);

            dbContext.Users.Remove(user);

            dbContext.SaveChanges();

            return true;
        }

        public void Save(User item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            using DbContext dbContext = new DbContext();

            User user = dbContext.Users.Find(item.Id);
            dbContext.Users.Remove(user);
            dbContext.Users.Add(item);

            dbContext.SaveChanges();           
        }
    }
}
