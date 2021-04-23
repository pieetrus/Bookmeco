using Application.DTOs;
using Application.Reservations.Commands.CreateReservation;
using Application.Reservations.Commands.DeleteReservation;
using Application.Reservations.Commands.UpdateReservation;
using Application.Reservations.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ReservationsController : BaseController
    {
        [SwaggerOperation(Summary = "Get reservations list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetReservationsListQuery()));
        }

        [SwaggerOperation(Summary = "Get reservation details")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationDto>> Get(int id)
        {
            var company = await Mediator.Send(new GetReservationDetailQuery { Id = id });

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Create reservation")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ReservationDto>> Create([FromBody] CreateReservationCommand command)
        {
            var reservation = await Mediator.Send(command);

            return Ok(reservation);
        }

        [SwaggerOperation(Summary = "Update reservation")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationDto>> Update([FromBody] UpdateReservationCommand command)
        {
            var reservation = await Mediator.Send(command);

            return Ok(reservation);
        }

        [SwaggerOperation(Summary = "Delete reservation")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteReservationCommand { Id = id });

            return NoContent();
        }
    }
}
