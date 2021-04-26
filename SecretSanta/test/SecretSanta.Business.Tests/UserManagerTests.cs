using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Business.Tests {
  [TestClass]
  public class UserManagerTests {
    [TestMethod]
    public void Create_WithUserData_ReturnsUserData() {
      UserManager manager = new();
      User myUser = new User() {Id = 19, FirstName = "William", LastName = "Burroughs"};

      User createdUser = manager.Create(myUser);

      Assert.IsTrue(createdUser.Id == myUser.Id);
      Assert.IsTrue(createdUser.FirstName == myUser.FirstName);
      Assert.IsTrue(createdUser.FirstName == myUser.FirstName);
    }

    [TestMethod]
    public void GetItem_WithId_ReturnsCorrectUser() {
      UserManager manager = new();

      User foundUser = manager.GetItem(1);

      Assert.IsTrue(foundUser.Id == 1);
    }

    [TestMethod]
    public void List_WithDataInList_ReturnsDataInList() {
      UserManager manager = new();
      ICollection<User> expectedData = DeleteMe.Users;

      ICollection<User> listData = manager.List();

      Assert.IsTrue(listData == expectedData);
    }

    [TestMethod]
    public void Remove_WithId_ReturnsTrue() {
      UserManager manager = new();

      Assert.IsTrue(manager.Remove(1));
    }

    [TestMethod]
    public void Remove_WithNegativeId_ReturnsFalse() {
      UserManager manager = new();

      Assert.IsFalse(manager.Remove(-1));
    }

    [TestMethod]
    public void Save_WithUserData_UpdatesUser() {
      UserManager manager = new();
      User myUser = new User() {Id = 1, FirstName = "William", LastName = "Burroughs"};

      manager.Save(myUser);

      Assert.IsTrue(manager.GetItem(1).FirstName == "William");
      Assert.IsTrue(manager.GetItem(1).LastName == "Burroughs");
    }
  }
}
