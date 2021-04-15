using Application.DTOs;
using Application.Users.Commands.Register;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        [SwaggerOperation(Summary = "Endpoint for login. Returns token.")]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginQuery query)
        {
            return await Mediator.Send(query);
        }

        [SwaggerOperation(Summary = "Endpoint for registration.")]
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<UserDto> Register(RegisterCommand command)
        {
            return await Mediator.Send(command);
        }


        [SwaggerOperation(Summary = "Assign user (worker) to service categories.")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> AssignUserToServiceCategories(int userId, UpdateUserCommand command)
        {
            command.UserId = userId;
            await Mediator.Send(command);

            return NoContent();
        }


        //[HttpGet]
        //public async Task<ActionResult<User>> CurrentUser()
        //{
        //    return await Mediator.Send(new GetCurrentUserQuery());
        //}
    }
}
