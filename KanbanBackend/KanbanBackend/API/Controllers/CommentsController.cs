using KanbanBackend.Application.Comments.Commands.CreateComment;
using KanbanBackend.Application.Comments.Commands.UpdateComment;
using KanbanBackend.Application.Comments.Commands.DeleteComment;
using KanbanBackend.Application.Comments.Queries.GetCommentsForTask;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KanbanBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentCommand command)
        {
            var dto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCommentsForTask), new { taskId = dto.TaskId }, dto);
        }

        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetCommentsForTask(int taskId)
        {
            var result = await _mediator.Send(new GetCommentsForTaskQuery(taskId));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, UpdateCommentCommand command)
        {
            if (id != command.Id) return BadRequest();
            var updated = await _mediator.Send(command);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _mediator.Send(new DeleteCommentCommand(id));
            return NoContent();
        }
    }
}
