using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers {
  [TestClass]
  public class UserControllerTests {
    [TestMethod]
    public void Constructor_WithNullUserManager_ThrowsAppropriateException() {
      ArgumentNullException e = Assert.ThrowsException<ArgumentNullException>(
        () => new UserController(null!)
      );

      Assert.AreEqual("userManager", e.ParamName);
    }

    [TestMethod]
    public void Get_WithData_ReturnsUsers() {
      UserController controller = new(new UserManager());

      IEnumerable<User> users = controller.Get();

      Assert.IsTrue(users.Any());
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(4)]
    public void Get_WithId_ReturnsUserManagerUser(int id) {
      UserManager manager = new();
      UserController controller = new(manager);
      User expectedUser = DeleteMe.Users.FirstOrDefault(x => x.Id == id);

      ActionResult<User?> result = controller.Get(id);

      Assert.AreEqual(id, result.Value.Id);
      Assert.AreEqual(expectedUser, result.Value);
    }

    [TestMethod]
    public void Get_WithNegativeId_ReturnsNotFound() {
      UserManager manager = new();
      UserController controller = new(manager);

      ActionResult<User?> result = controller.Get(-1);

      Assert.IsTrue(result.Result is NotFoundResult);
    }

    [TestMethod]
    public void Delete_WithId_ReturnsOk() {
      UserManager manager = new();
      UserController controller = new(manager);

      ActionResult result = controller.Delete(1);

      Assert.IsTrue(result is OkResult);
    }

    [TestMethod]
    public void Delete_WithNegativeId_ReturnsNotFound() {
      UserManager manager = new();
      UserController controller = new(manager);

      ActionResult result = controller.Delete(-1);

      Assert.IsTrue(result is NotFoundResult);
    }

    [TestMethod]
    public void Post_WithUserData_ReturnsUserData() {
      UserManager manager = new();
      UserController controller = new(manager);
      User myUser = new User() {Id = 19, FirstName = "William", LastName = "Burroughs"};

      User createdUser = controller.Post(myUser).Value;

      Assert.IsTrue(createdUser.Id == myUser.Id);
      Assert.IsTrue(createdUser.FirstName == myUser.FirstName);
      Assert.IsTrue(createdUser.FirstName == myUser.FirstName);
    }

    [TestMethod]
    public void Post_WithNullData_ReturnsBadRequest() {
      UserManager manager = new();
      UserController controller = new(manager);
      User myUser = null;

      ActionResult<User?> result = controller.Post(myUser);

      Assert.IsTrue(result.Result is BadRequestResult);
    }

    [TestMethod]
    public void Put_WithIdAndData_ReturnsOk() {
      UserManager manager = new();
      UserController controller = new(manager);
      User myUser = new User() {Id = 19, FirstName = "William", LastName = "Burroughs"};

      ActionResult result = controller.Put(4, myUser);

      Assert.IsTrue(result is OkResult);
    }

    [TestMethod]
    public void Put_WithIdAndNullData_ReturnsBadRequest() {
      UserManager manager = new();
      UserController controller = new(manager);
      User myUser = null;

      ActionResult result = controller.Put(4, myUser);

      Assert.IsTrue(result is BadRequestResult);
    }

    [TestMethod]
    public void Put_WithNegativeIdAndData_ReturnsNotFound() {
      UserManager manager = new();
      UserController controller = new(manager);
      User myUser = new User() {Id = 19, FirstName = "William", LastName = "Burroughs"};

      ActionResult result = controller.Put(-1, myUser);

      Assert.IsTrue(result is NotFoundResult);
    }
  }
}
