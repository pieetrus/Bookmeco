using Application.Companies.Commands.CreateCompany;
using Application.Companies.Commands.DeleteCompany;
using Application.Companies.Commands.UpdateCompany;
using Application.Companies.Queries;
using Application.CompaniesContent.Commands.CreateCompanyContent;
using Application.CompaniesContent.Commands.UpdateCompanyContent;
using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CompaniesController : BaseController
    {
        [SwaggerOperation(Summary = "Get companies list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCompaniesListQuery()));
        }

        [SwaggerOperation(Summary = "Get company detail")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyDto>> Get(int id)
        {
            var company = await Mediator.Send(new GetCompanyDetailQuery { Id = id });

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Add company")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [SwaggerOperation(Summary = "Update company")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyCommand command)
        {
            command.Id = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete company")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCompanyCommand { Id = id });

            return NoContent();
        }


        [SwaggerOperation(Summary = "Add company content for company")]
        [HttpPost("{companyId}/companyContent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateContent(int companyId, [FromBody] CreateCompanyContentCommand command)
        {
            command.CompanyId = companyId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [SwaggerOperation(Summary = "Update company content for company")]
        [HttpPut("{companyId}/companyContent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateContent(int companyId, [FromBody] UpdateCompanyContentCommand command)
        {
            command.CompanyId = companyId;
            var id = await Mediator.Send(command);

            return NoContent();
        }


        [SwaggerOperation(Summary = "Delete company content for company")]
        [HttpDelete("{id}/companyContent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCompanyContent(int id)
        {
            await Mediator.Send(new DeleteCompanyCommand { Id = id });

            return NoContent();
        }
    }
}
