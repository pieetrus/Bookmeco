using Application.DTOs;
using Application.ScheduleDays.Commands.CreateScheduleDay;
using Application.ScheduleDays.Commands.DeleteScheduleDay;
using Application.ScheduleDays.Commands.UpdateScheduleDay;
using Application.ScheduleDays.Queries;
using Application.Schedules.Commands.CreateSchedule;
using Application.Schedules.Commands.DeleteSchedule;
using Application.Schedules.Commands.UpdateSchedule;
using Application.Schedules.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class SchedulesController : BaseController
    {
        [SwaggerOperation(Summary = "Get schedules list")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllSchedules()
        {
            return Ok(await Mediator.Send(new GetSchedulesListQuery()));
        }

        [SwaggerOperation(Summary = "Get schedule details")]
        [HttpGet("{scheduleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDto>> GetSchedule(int scheduleId)
        {
            var company = await Mediator.Send(new GetScheduleDetailQuery { Id = scheduleId });

            return Ok(company);
        }

        [SwaggerOperation(Summary = "Create schedule")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [SwaggerOperation(Summary = "Update schedule")]
        [HttpPut("{scheduleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSchedule(int scheduleId, [FromBody] UpdateScheduleCommand command)
        {
            command.Id = scheduleId;

            await Mediator.Send(command);

            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete schedule")]
        [HttpDelete("{scheduleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSchedule(int scheduleId)
        {
            await Mediator.Send(new DeleteScheduleCommand { Id = scheduleId });

            return NoContent();
        }

        [SwaggerOperation(Summary = "Get list of all days in schedule")]
        [Route("{scheduleId}/scheduleDays")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDayDto>>> GetScheduleDays(int scheduleId)
        {
            return Ok(await Mediator.Send(new GetScheduleDaysListQuery { ScheduleId = scheduleId }));
        }

        [SwaggerOperation(Summary = "Get detailed day in schedule")]
        [Route("scheduleDays/{scheduleDayId}")]
        [HttpGet]
        public async Task<ActionResult<ScheduleDayDto>> GetScheduleDay(int scheduleDayId)
        {
            return Ok(await Mediator.Send(new GetScheduleDayDetailQuery { Id = scheduleDayId }));
        }


        [SwaggerOperation(Summary = "Create day in schedule")]
        [Route("{scheduleId}/scheduleDays")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateScheduleDay(int scheduleId, [FromBody] CreateScheduleDayCommand command)
        {
            command.ScheduleId = scheduleId;

            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [SwaggerOperation(Summary = "Update day in schedule")]
        [Route("scheduleDays")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateScheduleDay([FromBody] UpdateScheduleDayCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete schedule day")]
        [Route("scheduleDays/{scheduleDayId}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteScheduleDay(int scheduleDayId)
        {
            await Mediator.Send(new DeleteScheduleDayCommand { Id = scheduleDayId });

            return NoContent();
        }

    }
}
