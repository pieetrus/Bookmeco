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
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<OpinionDto>>> GetAll([FromQuery] int? reservationId)
        {
            var query = new GetOpinionsListQuery();
            query.ReservationId = reservationId;

            return Ok(await Mediator.Send(query));
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
        public async Task<ActionResult<OpinionDto>> Create([FromBody] CreateOpinionCommand command)
        {
            var opinion = await Mediator.Send(command);

            return Ok(opinion);
        }

        [SwaggerOperation(Summary = "Update opinion")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OpinionDto>> Update([FromBody] UpdateOpinionCommand command)
        {
            var opinion = await Mediator.Send(command);

            return Ok(opinion);
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
