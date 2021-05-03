using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using SecretSanta.Api.Dto;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SecretSanta.Api.Tests.Business;
using Newtonsoft.Json;

namespace SecretSanta.Api.Tests.Controllers {
  [TestClass]
  public class UsersControllerTests {
    [TestMethod]
    public void Constructor_WithNullUserRepo_ThrowsAppropriateException() {
      ArgumentNullException ex = Assert.ThrowsException<ArgumentNullException>(
        () => new UsersController(null!));
      Assert.AreEqual("repository", ex.ParamName);
    }

    [TestMethod]
    public async Task Get_WithData_ReturnsUsers() {
      WebApplicationFactory factory = new();
      TestableUserRepository repo = factory.Repo;
      UsersController controller = new(repo);
      HttpClient client = factory.CreateClient();

      HttpResponseMessage response = await client.GetAsync("/api/users");
      string json = await response.Content.ReadAsStringAsync();

//    This is the response that appears in the browser. Works just fine with the string hardcoded. 
//    Doesn't work when trying to parse the actual response. ReadAsStringAsync must be returning garbage JSON somehow.
//
//    string json = @"[{""id"":1,""firstName"":""Inigo"",""lastName"":""Montoya""},{""id"":2,""firstName"":""Princess"",""lastName"":""Buttercup""},{""id"":3,""firstName"":""Countz"",""lastName"":""Rugen""},{""id"":4,""firstName"":""Count"",""lastName"":""Rugen""},{""id"":5,""firstName"":""Miracle"",""lastName"":""Max""}]";

      List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

      Assert.IsTrue(users.Any());
    }

//  Not sure why this one fails. Always returns Id = 0, FirstName = "", LastName = "". Works fine in browser.
    [TestMethod]
    [DataRow(1)]
    [DataRow(4)]
    public async Task Get_WithId_ReturnsUser(int id) {
      WebApplicationFactory factory = new();
      TestableUserRepository repo = factory.Repo;
      UsersController controller = new(repo);
      HttpClient client = factory.CreateClient();
      User expectedUser = new();
      repo.GetItemUser = expectedUser;

      HttpResponseMessage response = await client.GetAsync("/api/users/1");
      string json = await response.Content.ReadAsStringAsync();
      int userId = (int) (long) JsonConvert.DeserializeObject<Dictionary<string, object>>(json)["id"];
      User user = JsonConvert.DeserializeObject<User>(json);

      Assert.AreEqual(1, userId);
      Assert.AreEqual(expectedUser, user);
    }

// Somehow this JSON parsing works when none of the others do. Weird.
    [TestMethod]
    public async Task Get_WithNegativeId_ReturnsNotFound() {
      WebApplicationFactory factory = new();
      TestableUserRepository repo = factory.Repo;
      UsersController controller = new(repo);
      HttpClient client = factory.CreateClient();

      HttpResponseMessage response = await client.GetAsync("/api/users/-1");
      string json = await response.Content.ReadAsStringAsync();
      int statusCode = (int) (long) JsonConvert.DeserializeObject<Dictionary<string, object>>(json)["status"];

      Assert.AreEqual(404, statusCode);
    }

    [TestMethod]
    public async Task Put_WithValidData_UpdatesUser() {
      WebApplicationFactory factory = new();
      TestableUserRepository repo = factory.Repo;
      User foundUser = new User
      {
          Id = 42
      };
      repo.GetItemUser = foundUser;
      HttpClient client = factory.CreateClient();
      UserUpdate update = new()
      {
          FirstName = "Joe",
          LastName = "Grassl"
      };

      HttpResponseMessage response = await client.PutAsJsonAsync("/api/users/42", update);

      response.EnsureSuccessStatusCode();
      Assert.AreEqual("Joe", repo.SavedUser?.FirstName);
      Assert.AreEqual("Grassl", repo.SavedUser?.LastName);
    }
  }
}
