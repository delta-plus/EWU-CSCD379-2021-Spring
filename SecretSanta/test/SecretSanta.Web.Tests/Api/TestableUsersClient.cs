using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Tests.Api {
  public class TestableUsersClient : IUsersClient {
    public Task DeleteAsync(int id) {
      throw new System.NotImplementedException();
    }
  
    public Task DeleteAsync(int id, CancellationToken cancellationToken) {
      throw new System.NotImplementedException();
    }
  
    public List<User> GetAllUsersReturnValue { get; set; } = new();
    public int GetAllAsyncInvocationCount { get; set; }
    public Task<ICollection<User>?> GetAllAsync() {
      GetAllAsyncInvocationCount++;
      return Task.FromResult<ICollection<User>?>(GetAllUsersReturnValue);
    }
  
    public Task<ICollection<User>> GetAllAsync(CancellationToken cancellationToken) {
      throw new System.NotImplementedException();
    }
  
    public Task<User> GetAsync(int id) {
      throw new System.NotImplementedException();
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
  
    public Task PutAsync(int id, UserUpdate update) {
      throw new System.NotImplementedException();
    }
  
    public Task PutAsync(int id, UserUpdate update, CancellationToken cancellationToken) {
      throw new System.NotImplementedException();
    }
  }
}
