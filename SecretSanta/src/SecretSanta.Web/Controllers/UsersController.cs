using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using SecretSanta.Web.Data;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        public IUsersClient Client { get; }

        public UsersController(IUsersClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IActionResult> Index()
        {
            ICollection<User> users = await Client.GetAllAsync();
            List<UserViewModel> userViewModels = new();

            foreach (User u in users) {
              userViewModels.Add(new UserViewModel {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
              });
            }

            return View(userViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserObject newUser = new();
                newUser.Id = viewModel.Id ?? 0;
                newUser.FirstName = viewModel.FirstName;
                newUser.LastName = viewModel.LastName;

                await Client.PostAsync(newUser);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            return View(MockData.Users[id]);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int id = viewModel.Id ?? 0;
                UserUpdate update = new();
                update.FirstName = viewModel.FirstName;
                update.LastName = viewModel.LastName;

                await Client.PutAsync(id, update);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await Client.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
