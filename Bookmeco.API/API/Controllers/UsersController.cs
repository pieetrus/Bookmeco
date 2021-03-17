using Application.Users;
using Application.Users.Commands.Register;
using Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginQuery query)
        {
            return await Mediator.Send(query);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<UserDto> Register(RegisterCommand command)
        {
            return await Mediator.Send(command);
        }

        //[HttpGet]
        //public async Task<ActionResult<User>> CurrentUser()
        //{
        //    return await Mediator.Send(new GetCurrentUserQuery());
        //}
    }
}
