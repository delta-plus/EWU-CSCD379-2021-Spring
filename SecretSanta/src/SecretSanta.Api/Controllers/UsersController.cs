using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Api.Dto;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository Repository { get; }

        public UsersController(IUserRepository repository)
        {
            Repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public IEnumerable<UserObject> Get()
        {
            UserList list = new();
            list.List = new();

            foreach (User user in Repository.List()) {
              UserObject userObj = new();
              userObj.Id = user.Id;
              userObj.FirstName = user.FirstName;
              userObj.LastName = user.LastName;
              list.List.Add(userObj);
            }

            return list.List;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public ActionResult<UserObject?> Get(int id)
        {
            User? user = Repository.GetItem(id);
            if (user is null) return NotFound();
            UserObject userObj = new();
            userObj.Id = user.Id;
            userObj.FirstName = user.FirstName;
            userObj.LastName = user.LastName;

            return userObj;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Delete(int id)
        {
            if (Repository.Remove(id))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public ActionResult<UserObject?> Post([FromBody] UserObject? user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            User newUser = new();
            newUser.Id = user.Id;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;

            newUser = Repository.Create(newUser);

            UserObject userObj = new();
            userObj.Id = newUser.Id;
            userObj.FirstName = newUser.FirstName;
            userObj.LastName = newUser.LastName;

            return userObj;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Put(int id, [FromBody] UserUpdate? update)
        {
            if (update is null)
            {
                return BadRequest();
            }

            User? foundUser = Repository.GetItem(id);

            if (foundUser is not null)
            {
                foundUser.FirstName = update.FirstName ?? "";
                foundUser.LastName = update.LastName ?? "";

                Repository.Save(foundUser);
                return Ok();
            }

            return NotFound();
        }
    }
}
