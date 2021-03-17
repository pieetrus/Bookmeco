using Application.Companies;
using Application.Companies.Commands.CreateCompany;
using Application.Companies.Commands.DeleteCompany;
using Application.Companies.Commands.UpdateCompany;
using Application.Companies.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;

namespace API.Controllers
{
    public class CompaniesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCompaniesListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyDto>> Get(int id)
        {
            var company = await Mediator.Send(new GetCompanyDetailQuery { Id = id });

            return Ok(company);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyCommand command)
        {
            command.Id = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCompanyCommand { Id = id });

            return NoContent();
        }
    }
}
