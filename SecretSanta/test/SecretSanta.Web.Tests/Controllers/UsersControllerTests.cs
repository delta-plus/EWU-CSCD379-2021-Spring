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
      usersClient.GetAllUsersReturnValue = new List<User>() {
        user1,
        user2
      };

      HttpResponseMessage response = await client.GetAsync("/Users");

      response.EnsureSuccessStatusCode();
      Assert.AreEqual(1, usersClient.GetAllAsyncInvocationCount);
    }

    [TestMethod]
    public async Task Edit_WithValidModel_InvokesPostAsync() {
      HttpClient client = Factory.CreateClient();
      TestableUsersClient usersClient = Factory.Client;
      Dictionary<string, string?> values = new() {
        { nameof(UserViewModel.FirstName), "Joe" },
        { nameof(UserViewModel.LastName), "Grassl" }
      };
      FormUrlEncodedContent content = new(values!);

      HttpResponseMessage response = await client.PostAsync("/Users/Create", content);

      response.EnsureSuccessStatusCode();
      Assert.AreEqual(1, usersClient.PostAsyncInvocationCount);
      Assert.AreEqual(1, usersClient.PostAsyncInvokedParameters.Count);
      Assert.AreEqual("Joe", usersClient.PostAsyncInvokedParameters[0].FirstName);
      Assert.AreEqual("Grassl", usersClient.PostAsyncInvokedParameters[0].LastName);
    }
  }
}
