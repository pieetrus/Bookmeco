using Application.DTOs;
using Application.Opinions.Commands.CreateOpinion;
using Application.Opinions.Commands.DeleteOpinion;
using Application.Opinions.Commands.UpdateOpinion;
using Application.Opinions.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class OpinionsController : BaseController
    {
        [SwaggerOperation(Summary = "Get opinions list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpinionDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetOpinionsListQuery()));
        }

        [SwaggerOperation(Summary = "Get opinion details")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OpinionDto>> Get(int id)
        {
            var company = await Mediator.Send(new GetOpinionDetailQuery { Id = id });

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Create opinion")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateOpinionCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [SwaggerOperation(Summary = "Update opinion")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOpinionCommand command)
        {
            command.Id = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete opinion")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteOpinionCommand { Id = id });

            return NoContent();
        }
    }
}
