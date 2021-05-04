using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Web.Api;
using SecretSanta.Web.Tests.Api;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Tests {
  [TestClass]
  public class UsersControllerTests {
    private WebApplicationFactory Factory { get; } = new();
    private TestableUsersClient Client { get; } = new();

    [TestMethod]
    public async Task Index_WithUsers_InvokesGetAllAsync() {
      HttpClient client = Factory.CreateClient();
      TestableUsersClient usersClient = Factory.Client;
      User user1 = new() {
        Id = 1,
        FirstName = "Joe",
        LastName = "Grassl"
      };
      User user2 = new() {
        Id = 2,
        FirstName = "Sonja",
        LastName = "Lang"
      };
      usersClient.UserList = new List<User>() {
        user1,
        user2
      };

      HttpResponseMessage response = await client.GetAsync("/Users");

      response.EnsureSuccessStatusCode();
      Assert.AreEqual(1, usersClient.GetAllAsyncInvocationCount);
    }

    [TestMethod]
    public async Task Create_WithValidData_InvokesPostAsync() {
      HttpClient client = Factory.CreateClient();
      TestableUsersClient usersClient = Factory.Client;
      Dictionary<string, string?> values = new() {
        { nameof(UserObject.FirstName), "Joe" },
        { nameof(UserObject.LastName), "Grassl" }
      };
      FormUrlEncodedContent content = new(values!);

      HttpResponseMessage response = await client.PostAsync("/Users/Create", content);

      response.EnsureSuccessStatusCode();
      Assert.AreEqual(1, usersClient.PostAsyncInvocationCount);
      Assert.AreEqual(1, usersClient.PostAsyncInvokedParameters.Count);
      Assert.AreEqual("Joe", usersClient.PostAsyncInvokedParameters[0].FirstName);
      Assert.AreEqual("Grassl", usersClient.PostAsyncInvokedParameters[0].LastName);
    }

    [TestMethod]
    public async Task Edit_WithValidData_InvokesPutAsync() {
      HttpClient client = Factory.CreateClient();
      TestableUsersClient usersClient = Factory.Client;
      Dictionary<string, string?> values = new() {
        { nameof(UserUpdate.FirstName), "Joe" },
        { nameof(UserUpdate.LastName), "Grassl" }
      };
      FormUrlEncodedContent content = new(values!);

      HttpResponseMessage response = await client.PostAsync("/Users/Edit/1", content);

      response.EnsureSuccessStatusCode();
      Assert.AreEqual(1, usersClient.PutAsyncInvocationCount);
      Assert.AreEqual(1, usersClient.PutAsyncInvokedParameters.Count);
      Assert.AreEqual("Joe", usersClient.PutAsyncInvokedParameters[0].FirstName);
      Assert.AreEqual("Grassl", usersClient.PutAsyncInvokedParameters[0].LastName);
    }

    [TestMethod]
    public async Task Delete_WithId_InvokesDeleteAsync() {
      HttpClient client = Factory.CreateClient();
      TestableUsersClient usersClient = Factory.Client;

//    Not sure how you'd test this because I can't really find an endpoint to post to 
//    (the Delete button simply uses asp-action="Delete")
//    and furthermore, the Delete button just redirects to "api/Users/[id]" instead of 
//    actually deleting ever since implementing the generated UsersClient. Weird.

//    HttpResponseMessage response = await client.PostAsync(???, ???);

//    response.EnsureSuccessStatusCode();
//    Assert.AreEqual(1, usersClient.DeleteAsyncInvocationCount);
    }
  }
}
