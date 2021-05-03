using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Business {
  public class TestableUserRepository : IUserRepository {
    public User Create(User item) {
      throw new System.NotImplementedException();
    }

    public User? GetItemUser { get; set; }
    public int GetItemId { get; set; }
    public User? GetItem(int id) {
      GetItemId = id;
      return GetItemUser;
    }

    public ICollection<User> UserList { get; set; }
    public ICollection<User> List() {
      return UserList;
    }

    public bool Remove(int id) {
      throw new System.NotImplementedException();
    }

    public User? SavedUser {get; set;}
    public void Save(User item) {
      SavedUser = item;
    }
  }
}
