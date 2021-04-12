using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace SecretSanta.Web.Controllers {
  public class GiftsController : Controller {
    public IActionResult Index() {
      return View(TestData.Gifts);
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    public IActionResult Create(GiftViewModel gift) {
      if (ModelState.IsValid) {
        // Give new gift a fresh ID.
        int lastID = 0;
        foreach (GiftViewModel model in TestData.Gifts) {
          if (model.ID > lastID) {
            lastID = model.ID;
          }
        }
        gift.ID = lastID + 1;

        TestData.Gifts.Add(gift);
        return RedirectToAction(nameof(Index));
      }

      return View(gift);
    }

    public IActionResult Edit(int id) {
      return View(TestData.Gifts[id]);
    }

    [HttpPost]
    public IActionResult Edit(GiftViewModel gift) {
      if (ModelState.IsValid) {
        TestData.Gifts[gift.ID] = gift;
        return RedirectToAction(nameof(Index));
      }

      return View(gift);
    }

    [HttpPost]
    public IActionResult Delete(int id) {
      TestData.Gifts.RemoveAt(id);
      return RedirectToAction(nameof(Index));
    }
  }
}
