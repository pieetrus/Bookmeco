using Application.DTOs;
using Application.UserCompanyAccessTypes.Commands.CreateUserCompanyAccessType;
using Application.UserCompanyAccessTypes.Commands.DeleteUserCompanyAccessType;
using Application.UserCompanyAccessTypes.Commands.UpdateUserCompanyAccessType;
using Application.UserCompanyAccessTypes.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UserCompanyAccessTypesController : BaseController
    {
        [SwaggerOperation(Summary = "Get user company access types list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCompanyAccessTypeDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetUserCompanyAccessTypesListQuery()));
        }

        [SwaggerOperation(Summary = "Get user company access type details")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserCompanyAccessTypeDto>> Get(int id)
        {
            var company = await Mediator.Send(new GetUserCompanyAccessTypesDetailQuery { Id = id });

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Create user company access type")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserCompanyAccessTypeDto>> Create([FromBody] CreateUserCompanyAccessTypeCommand command)
        {
            var userCompanyAccessType = await Mediator.Send(command);

            return Ok(userCompanyAccessType);
        }

        [SwaggerOperation(Summary = "Update user company access type")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserCompanyAccessTypeDto>> Update([FromBody] UpdateUserCompanyAccessTypeCommand command)
        {
            var userCompanyAccessType = await Mediator.Send(command);

            return Ok(userCompanyAccessType);
        }

        [SwaggerOperation(Summary = "Delete user company access type")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteUserCompanyAccessTypeCommand { Id = id });

            return NoContent();
        }
    }
}
