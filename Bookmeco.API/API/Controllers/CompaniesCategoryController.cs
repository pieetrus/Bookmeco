using Application.CompanyCategories.Commands.CreateCompanyCategory;
using Application.CompanyCategories.Commands.DeleteCompanyCategory;
using Application.CompanyCategories.Commands.UpdateCompanyCategory;
using Application.CompanyCategories.Queries;
using Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CompaniesCategoryController : BaseController
    {
        [SwaggerOperation(Summary = "Get companies category list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyCategoryDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCompanyCategoriesListQuery()));
        }

        [SwaggerOperation(Summary = "Get company category")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyCategoryDto>> Get(int id)
        {
            var company = await Mediator.Send(new GetCompanyCategoryDetailQuery { Id = id });

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Create company category")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCategoryCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [SwaggerOperation(Summary = "Update company category")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyCategoryCommand command)
        {
            command.Id = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete company category")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCompanyCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
