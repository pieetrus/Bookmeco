using Application.DTOs;
using Application.UserCompanies.Commands.CreateUserCompany;
using Application.UserCompanies.Commands.DeleteUserCompany;
using Application.UserCompanies.Commands.UpdateUserCompany;
using Application.UserCompanies.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UserCompaniesController : BaseController
    {
        [SwaggerOperation(Summary = "Get user companies list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCompanyDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetUserCompaniesListQuery()));
        }

        [SwaggerOperation(Summary = "Get user company details")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserCompanyDto>> Get(int id)
        {
            var company = await Mediator.Send(new GetUserCompaniesDetailQuery { Id = id });

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Create user company")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserCompanyDto>> Create([FromBody] CreateUserCompanyCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [SwaggerOperation(Summary = "Update user company")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserCompanyDto>> Update([FromBody] UpdateUserCompanyCommand command)
        {
            var companyCategory = await Mediator.Send(command);

            return Ok(companyCategory);
        }

        [SwaggerOperation(Summary = "Delete user company")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteUserCompanyCommand { Id = id });

            return NoContent();
        }
    }
}
