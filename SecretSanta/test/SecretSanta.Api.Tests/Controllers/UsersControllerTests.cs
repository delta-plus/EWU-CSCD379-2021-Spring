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
      repo.UserList = new List<User>() {
        new User() {
          Id = 1,
          FirstName = "Joe",
          LastName = "Grassl"
        },
        new User() {
          Id = 2,
          FirstName = "Sonja",
          LastName = "Lang"
        }
      };

      HttpResponseMessage response = await client.GetAsync("/api/users");

      response.EnsureSuccessStatusCode();
      Assert.IsTrue(repo.UserList.Any());
    }

    [TestMethod]
    public async Task Get_WithId_ReturnsUser() {
      WebApplicationFactory factory = new();
      TestableUserRepository repo = factory.Repo;
      UsersController controller = new(repo);
      HttpClient client = factory.CreateClient();
      User expectedUser = new User() {
        Id = 1,
        FirstName = "Joe",
        LastName = "Grassl"
      };
      repo.GetItemUser = expectedUser;

      HttpResponseMessage response = await client.GetAsync("/api/users/1");

      response.EnsureSuccessStatusCode();
      Assert.AreEqual(1, repo.GetItem(1).Id);
      Assert.AreEqual(expectedUser, repo.GetItem(1));
    }

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
    public async Task Post_WithValidData_CreatesUser() {
      WebApplicationFactory factory = new();
      TestableUserRepository repo = factory.Repo;
      UsersController controller = new(repo);
      HttpClient client = factory.CreateClient();
      repo.UserList = new();

      UserObject userObj = new() {
        Id = 1,
        FirstName = "Joe",
        LastName = "Grassl"
      };

      HttpResponseMessage response = await client.PostAsJsonAsync("/api/users", userObj);

      response.EnsureSuccessStatusCode();
      Assert.AreEqual(1, repo.UserList[0].Id);
      Assert.AreEqual("Joe", repo.UserList[0].FirstName);
      Assert.AreEqual("Grassl", repo.UserList[0].LastName);
    }

    [TestMethod]
    public async Task Delete_WithId_RemovesUser() {
      WebApplicationFactory factory = new();
      TestableUserRepository repo = factory.Repo;
      UsersController controller = new(repo);
      HttpClient client = factory.CreateClient();
      repo.UserList = new List<User>() {
        new User() {
          Id = 1,
          FirstName = "Joe",
          LastName = "Grassl"
        },
        new User() {
          Id = 2,
          FirstName = "Sonja",
          LastName = "Lang"
        }
      };

      HttpResponseMessage response = await client.DeleteAsync("/api/users/1");

      response.EnsureSuccessStatusCode();
      Assert.AreEqual(2, repo.UserList[0].Id);
      Assert.AreEqual("Sonja", repo.UserList[0].FirstName);
      Assert.AreEqual("Lang", repo.UserList[0].LastName);
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
