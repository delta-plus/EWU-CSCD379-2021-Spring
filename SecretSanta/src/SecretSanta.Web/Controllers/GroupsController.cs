using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace SecretSanta.Web.Controllers {
  public class GroupsController : Controller {
    public IActionResult Index() {
      return View(TestData.Groups);
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    public IActionResult Create(GroupViewModel group) {
      if (ModelState.IsValid) {
        // Give new group a fresh ID.
        int lastID = 0;
        foreach (GroupViewModel model in TestData.Groups) {
          if (model.ID > lastID) {
            lastID = model.ID;
          }
        }
        group.ID = lastID + 1;

        TestData.Groups.Add(group);
        return RedirectToAction(nameof(Index));
      }

      return View(group);
    }

    public IActionResult Edit(int id) {
      TestData.Groups[id].ID = id;
      return View(TestData.Groups[id]);
    }

    [HttpPost]
    public IActionResult Edit(GroupViewModel group) {
      if (ModelState.IsValid) {
        TestData.Groups[group.ID] = group;
        return RedirectToAction(nameof(Index));
      }

      return View(group);
    }

    [HttpPost]
    public IActionResult Delete(int id) {
      TestData.Groups.RemoveAt(id);
      return RedirectToAction(nameof(Index));
    }
  }
}
