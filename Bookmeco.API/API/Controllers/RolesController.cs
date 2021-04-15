using Application.DTOs;
using Application.Roles.Commands.CreateRole;
using Application.Roles.Commands.DeleteRole;
using Application.Roles.Commands.UpdateRole;
using Application.Roles.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class RolesController : BaseController
    {
        [SwaggerOperation(Summary = "Get roles list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetRolesListQuery()));
        }

        [SwaggerOperation(Summary = "Get role details")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDto>> Get(int id)
        {
            var company = await Mediator.Send(new GetRoleDetailQuery { Id = id });

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Create role")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [SwaggerOperation(Summary = "Update role")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleCommand command)
        {
            command.Id = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete role")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteRoleCommand { Id = id });

            return NoContent();
        }
    }
}
