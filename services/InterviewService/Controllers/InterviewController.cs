using InterviewService.Commands.FinishInterview;
using InterviewService.Commands.ScheduleInterview;
using InterviewService.Commands.StartInterview;
using InterviewService.Queries.GetInterview;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InterviewService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InterviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> Schedule([FromBody] ScheduleInterviewCommand command)
        {
            var id = await _mediator.Send(command);

            return Ok(id);
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> Start(Guid id)
        {
            await _mediator.Send(new StartInterviewCommand(id));
            return NoContent();
        }

        [HttpPost("{id}/finish")]
        public async Task<IActionResult> Finish(Guid id)
        {
            await _mediator.Send(new FinishInterviewCommand(id));
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetInterviewQuery(id));
            return Ok(result);
        }

    }
}
