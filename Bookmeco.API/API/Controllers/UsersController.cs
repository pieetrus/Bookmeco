using Application.DTOs;
using Application.Users.Commands.AssignRoles;
using Application.Users.Commands.AssignServiceCategories;
using Application.Users.Commands.Register;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries;
using Application.Users.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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


        [SwaggerOperation(Summary = "Update user.")]
        [HttpPut]
        public async Task<ActionResult<UserDto>> Update(UpdateUserCommand command)
        {
            var user = await Mediator.Send(command);

            return Ok(user);
        }

        [SwaggerOperation(Summary = "Get users/workers list.")]
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetList(int? companyId)
        {
            return await Mediator.Send(new GetUsersListQuery(companyId));
        }


        [SwaggerOperation(Summary = "Assign roles to user")]
        [HttpPut("roles")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<RoleDto>> AssignRolesToUser([FromBody] AssignRolesCommand command)
        {
            var role = await Mediator.Send(command);

            return Ok(role);
        }

        [SwaggerOperation(Summary = "Assign service categories to user (worker)")]
        [HttpPut("serviceCategories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<RoleDto>> AssignServiceCategoriesToUser([FromBody] AssignServiceCategoriesCommand command)
        {
            var role = await Mediator.Send(command);

            return Ok(role);
        }
    }
}
