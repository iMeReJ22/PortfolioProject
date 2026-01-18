using KanbanBackend.Application.Columns.Commands.CreateColumn;
using KanbanBackend.Application.Columns.Commands.UpdateColumn;
using KanbanBackend.Application.Columns.Commands.DeleteColumn;
using KanbanBackend.Application.Columns.Commands.ReorderColumns;
using KanbanBackend.Application.Columns.Queries.GetColumnsForBoard;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KanbanBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ColumnsController : Controller
    {
        private readonly IMediator _mediator;
        public ColumnsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateColumn(CreateColumnCommand command)
        {
            var dto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetColumnsForBoard), new { boardId = dto.BoardId }, dto);
        }

        [HttpGet("board/{boardId}")]
        public async Task<IActionResult> GetColumnsForBoard(int boardId)
        {
            var result = await _mediator.Send(new GetColumnsForBoardQuery(boardId));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateColumn(int id, UpdateColumnCommand command)
        {
            if (id != command.Id) return BadRequest();
            var updated = await _mediator.Send(command);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumn(int id)
        {
            await _mediator.Send(new DeleteColumnCommand(id));
            return NoContent();
        }

        [HttpPost("board/{boardId}/reorder")]
        public async Task<IActionResult> ReorderColumns(int boardId, ReorderColumnsCommand command)
        {
            if (boardId != command.BoardId) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
