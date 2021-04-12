using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace SecretSanta.Web.Controllers {
  public class UsersController : Controller {
    public IActionResult Index() {
      return View(TestData.Users);
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    public IActionResult Create(UserViewModel user) {
      if (ModelState.IsValid) {
        // Give new user a fresh ID.
        int lastID = 0;
        foreach (UserViewModel model in TestData.Users) {
          if (model.ID > lastID) {
            lastID = model.ID;
          }
        }
        user.ID = lastID + 1;

        TestData.Users.Add(user);
        return RedirectToAction(nameof(Index));
      }

      return View(user);
    }

    public IActionResult Edit(int id) {
      TestData.Users[id].ID = id;
      return View(TestData.Users[id]);
    }

    [HttpPost]
    public IActionResult Edit(UserViewModel user) {
      if (ModelState.IsValid) {
        TestData.Users[user.ID] = user;
        return RedirectToAction(nameof(Index));
      }

      return View(user);
    }

    [HttpPost]
    public IActionResult Delete(int id) {
      TestData.Users.RemoveAt(id);
      return RedirectToAction(nameof(Index));
    }
  }
}
