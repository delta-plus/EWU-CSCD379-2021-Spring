using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase {
    private IUserRepository UserManager { get; }

    public UserController(IUserRepository userManager) {
      UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    [HttpGet]
    public IEnumerable<User> Get() {
      return UserManager.List();
    }

    [HttpGet("{id}")]
    public ActionResult<User?> Get(int id) {
      if (id < 0) {
        return NotFound();
      }

      User? returnedUser = UserManager.GetItem(id);
      return returnedUser;
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id) {
      if (id < 0) {
        return NotFound();
      }

      if (UserManager.Remove(id)) {
        return Ok();
      }

      return NotFound();
    }

    [HttpPost]
    public ActionResult<User?> Post([FromBody] User? myUser) {
      if (myUser is null) {
        return BadRequest();
      }

      return UserManager.Create(myUser);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] User? myUser) {
      if (myUser is null) {
        return BadRequest();
      }

      User? foundUser = UserManager.GetItem(id);

      if (foundUser is not null) {
        if (!string.IsNullOrWhiteSpace(myUser.FirstName)) {
          foundUser.FirstName = myUser.FirstName;
        }

        if (!string.IsNullOrWhiteSpace(myUser.LastName)) {
          foundUser.LastName = myUser.LastName;
        }

        UserManager.Save(foundUser);
        return Ok();
      }

      return NotFound();
    }
  }
}
