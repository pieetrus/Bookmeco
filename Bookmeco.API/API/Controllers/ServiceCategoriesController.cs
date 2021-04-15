using Application.DTOs;
using Application.ServiceCategories.Commands.CreateServiceCategory;
using Application.ServiceCategories.Commands.DeleteServiceCategory;
using Application.ServiceCategories.Commands.UpdateServiceCategory;
using Application.ServiceCategories.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ServiceCategoriesController : BaseController
    {
        [SwaggerOperation(Summary = "Get service categories list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceCategoryDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetServiceCategoriesListQuery()));
        }

        [SwaggerOperation(Summary = "Get service category details")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceCategoryDto>> Get(int id)
        {
            var company = await Mediator.Send(new GetServiceCategoryDetailQuery { Id = id });

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Create service category")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateServiceCategoryCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [SwaggerOperation(Summary = "Update service category")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceCategoryCommand command)
        {
            command.Id = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete service category")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteServiceCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
