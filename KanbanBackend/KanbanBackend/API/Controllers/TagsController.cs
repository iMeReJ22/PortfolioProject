using KanbanBackend.Application.Tags.Commands.CreateTag;
using KanbanBackend.Application.Tags.Commands.UpdateTag;
using KanbanBackend.Application.Tags.Commands.DeleteTag;
using KanbanBackend.Application.Tags.Queries.GetTagsForBoard;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KanbanBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TagsController : Controller
    {
        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(CreateTagCommand command)
        {
            var dto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTagsForBoard), new { boardId = dto.BoardId }, dto);
        }

        [HttpGet("board/{boardId}")]
        public async Task<IActionResult> GetTagsForBoard(int boardId)
        {
            var result = await _mediator.Send(new GetTagsForBoardQuery(boardId));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, UpdateTagCommand command)
        {
            if (id != command.Id) return BadRequest();
            var updated = await _mediator.Send(command);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _mediator.Send(new DeleteTagCommand(id));
            return NoContent();
        }
    }
}
