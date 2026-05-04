using BasicAuth.Commands.Users.CreateUser;
using BasicAuth.Commands.Users.DeleteUser;
using BasicAuth.Commands.Users.UpdateUser;
using BasicAuth.Data.Models;
using BasicAuth.Queries.Users.GetUser;
using BasicAuth.Queries.Users.GetUsers;
using BasicAuth.Web.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth.Web.Controllers {

    [ApiController]
    [Route("users")]
    public class UsersController : Controller {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet()] // List all users
        public async Task<ActionResult<List<UserInfoDto>>> GetUsers() {
            var query = new GetUsersRequest();
            var users = await _mediator.Send(query);
            return users.Select(e => new UserInfoDto(e.Id, e.Firstname, e.Lastname, e.Email)).ToList();
        }

        [HttpGet("{id}")] // get user details with email
        public async Task<ActionResult<UserInfoDto>> GetUser(int id) {
            User? user = await _mediator.Send(new GetUserRequest((int)id));
            if (user is null) return NotFound();
            return Ok(new UserInfoDto(user.Id, user.Firstname, user.Lastname, user.Email));
        }

        [HttpPost] // create user
        public async Task<ActionResult> CreateUser(CreateUserRequest request) {
            if (await _mediator.Send(request)) return Ok();
            return BadRequest("Email already exists");
        }

        [HttpDelete("{id}")] // Delete User
        public async Task<ActionResult> DeleteUser(int id) {
            await _mediator.Send(new DeleteUserRequest(id));
            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult<UserInfoDto>> UpdateUser(UpdateUserRequest request) {
            var user = await _mediator.Send(request);
            if (user is null) return NotFound();
            return Ok(new UserInfoDto(user.Id, user.Firstname, user.Lastname, user.Email));
        }
    }
}
