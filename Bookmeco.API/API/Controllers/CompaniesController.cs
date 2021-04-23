using Application.Companies.Commands.CreateCompany;
using Application.Companies.Commands.DeleteCompany;
using Application.Companies.Commands.UpdateCompany;
using Application.Companies.Queries;
using Application.CompaniesContent.Commands.CreateCompanyContent;
using Application.CompaniesContent.Commands.DeleteCompanyContent;
using Application.CompaniesContent.Commands.UpdateCompanyContent;
using Application.CompaniesContent.Queries;
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
        public async Task<ActionResult<CompanyDto>> Create([FromBody] CreateCompanyCommand command)
        {
            var company = await Mediator.Send(command);

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Update company")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyDto>> Update([FromBody] UpdateCompanyCommand command)
        {
            var company = await Mediator.Send(command);

            return Ok(company);
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

        [SwaggerOperation(Summary = "Get company content detail")]
        [HttpGet("companyContent/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyContentDto>> GetCompanyContent(int id)
        {
            var company = await Mediator.Send(new GetCompanyContentDetailQuery { Id = id });

            return Ok(company);
        }


        [SwaggerOperation(Summary = "Add company content for company")]
        [HttpPost("{companyId}/companyContent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CompanyContentDto>> CreateContent(int companyId, [FromBody] CreateCompanyContentCommand command)
        {
            command.CompanyId = companyId;
            var companyContent = await Mediator.Send(command);

            return Ok(companyContent);
        }

        [SwaggerOperation(Summary = "Update company content")]
        [HttpPut("companyContent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CompanyContentDto>> UpdateContent([FromBody] UpdateCompanyContentCommand command)
        {
            var companyContent = await Mediator.Send(command);

            return Ok(companyContent);
        }


        [SwaggerOperation(Summary = "Delete company content")]
        [HttpDelete("companyContent/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCompanyContent(int id)
        {
            await Mediator.Send(new DeleteCompanyContentCommand { Id = id });

            return NoContent();
        }
    }
}
