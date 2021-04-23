using Application.DTOs;
using Application.Users.Commands.Register;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries;
using Application.Users.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        [SwaggerOperation(Summary = "Endpoint for login. Returns token.")]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginDto>> Login(LoginQuery query)
        {
            return await Mediator.Send(query);
        }

        [SwaggerOperation(Summary = "Endpoint for registration.")]
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<UserLoginDto> Register(RegisterCommand command)
        {
            return await Mediator.Send(command);
        }


        [SwaggerOperation(Summary = "Update user. Assign user (worker) to service categories.")]
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserDto>> Update(int userId, UpdateUserCommand command)
        {
            command.UserId = userId;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetList()
        {
            return await Mediator.Send(new GetUsersListQuery());
        }
    }
}
