using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Business {
  public class TestableUserRepository : IUserRepository {
    public List<User> UserList { get; set; }
    public User? GetItemUser { get; set; }
    public int GetItemId { get; set; }
    public User? SavedUser {get; set;}

    public User Create(User item) {
      UserList.Add(item);

      return item;
    }

    public User? GetItem(int id) {
      GetItemId = id;

      return GetItemUser;
    }

    public ICollection<User> List() {
      return UserList;
    }

    public bool Remove(int id) {
      User match = UserList.Find(x => x.Id == id);

      if (match != null) {
        UserList.Remove(match);

        return true;
      }

      return false;
    }

    public void Save(User item) {
      SavedUser = item;
    }
  }
}
