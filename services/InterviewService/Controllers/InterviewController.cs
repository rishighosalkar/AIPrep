using InterviewService.Application.Commands.FinishInterview;
using InterviewService.Application.Commands.StartInterview;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using InterviewService.Domain.Entities;
using InterviewService.Application.Queries.GetInterview;
using InterviewService.Application.Commands.ScheduleInterview;

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

        [HttpPost("start")]
        public async Task<IActionResult> Start([FromBody] StartInterviewRequest request)
        {
            var interviewId = await _mediator.Send(new StartInterviewCommand(request.CandidateId, request.Role));
            return Ok(new { interviewId });
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
