using KanbanBackend.Application.ActivityLog.Commands.CreateActivityLogTask;
using KanbanBackend.Application.ActivityLog.Queries.GetActivityForBoard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ActivityLogController : Controller
    {
        private readonly IMediator _mediator;
        public ActivityLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityForBoard(int id)
        {
            var result = await _mediator.Send(new GetActivityForBoardQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivityTask(CreateActivityLogTaskCommand command)
        {
            var dto = await _mediator.Send(command);
            return CreatedAtAction(null, new { id = dto.Id }, dto);
        }
    }
}
