using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Tests.Api {
  public class TestableUsersClient : IUsersClient {
    public int DeleteAsyncInvocationCount { get; set; }
    public List<User> UserList { get; set; } = new();
    public async Task DeleteAsync(int id) {
      DeleteAsyncInvocationCount++;
    }
  
    public Task DeleteAsync(int id, CancellationToken cancellationToken) {
      throw new System.NotImplementedException();
    }
    public int GetAllAsyncInvocationCount { get; set; } 
    public Task<ICollection<User>?> GetAllAsync() {
      GetAllAsyncInvocationCount++;

      return Task.FromResult<ICollection<User>?>(UserList);
    }
  
    public Task<ICollection<User>> GetAllAsync(CancellationToken cancellationToken) {
      throw new System.NotImplementedException();
    }

    // Not required because the Web UsersController doesn't use GetAsync.
    // This is what I imagine it would look like, though.
    public int GetAsyncInvocationCount { get; set; } 
    public Task<User> GetAsync(int id) {
      GetAsyncInvocationCount++;

      return Task.FromResult<User?>(UserList.Find(x => x.Id == id));
    }
  
    public Task<User> GetAsync(int id, CancellationToken cancellationToken) {
      throw new System.NotImplementedException();
    }
  
    public int PostAsyncInvocationCount { get; set; }
    public List<User> PostAsyncInvokedParameters { get; set; } = new();
    public Task<User> PostAsync(UserObject user) {
      PostAsyncInvocationCount++;
      User newUser = new();
      newUser.Id = user.Id;
      newUser.FirstName = user.FirstName;
      newUser.LastName = user.LastName;
      PostAsyncInvokedParameters.Add(newUser);

      return Task.FromResult(newUser);
    }
  
    public Task<User> PostAsync(UserObject user, CancellationToken cancellationToken) {
      throw new System.NotImplementedException();
    }

    public int PutAsyncInvocationCount { get; set; }
    public List<User> PutAsyncInvokedParameters { get; set; } = new();
    public async Task PutAsync(int id, UserUpdate update) {
      PutAsyncInvocationCount++;
      User extantUser = new() {
        Id = id,
        FirstName = "",
        LastName = ""
      };
      extantUser.FirstName = update.FirstName;
      extantUser.LastName = update.LastName;
      PutAsyncInvokedParameters.Add(extantUser);
    }
  
    public Task PutAsync(int id, UserUpdate update, CancellationToken cancellationToken) {
      throw new System.NotImplementedException();
    }
  }
}
