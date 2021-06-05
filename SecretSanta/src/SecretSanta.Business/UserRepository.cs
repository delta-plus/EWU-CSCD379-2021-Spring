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

//            MockData.Users[item.Id] = item;
            return item;
        }

        public User? GetItem(int id)
        {
//            if (MockData.Users.TryGetValue(id, out User? user))
//            {
//                return user;
//            }
//            return null;
            using DbContext dbContext = new DbContext();

            return dbContext.Users.Find(id);
        }

        public ICollection<User> List()
        {
//            return MockData.Users.Values;
            using DbContext dbContext = new DbContext();

            return dbContext.Users.ToList();
        }

        public bool Remove(int id)
        {
//            return MockData.Users.Remove(id);
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

//            MockData.Users[item.Id] = item;
        }
    }
}
