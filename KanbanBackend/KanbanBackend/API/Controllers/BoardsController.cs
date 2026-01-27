using KanbanBackend.Application.Boards.Commands.CreateBoard;
using KanbanBackend.Application.Boards.Commands.UpdateBoard;
using KanbanBackend.Application.Boards.Commands.DeleteBoard;
using KanbanBackend.Application.Boards.Commands.AddBoardMember;
using KanbanBackend.Application.Boards.Commands.RemoveBoard;
using KanbanBackend.Application.Boards.Queries.GetBoardById;
using KanbanBackend.Application.Boards.Queries.GetBoardsForUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KanbanBackend.Application.Boards.Queries.GetDashboardBoardsWithOwners;
using KanbanBackend.Application.Boards.Queries.GetDetailedBoardById;

namespace KanbanBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BoardsController : Controller
    {
        private readonly IMediator _mediator;
        public BoardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard(CreateBoardCommand command)
        {
            var dto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBoardById), new { id = dto.Id }, dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoardById(int id)
        {
            var result = await _mediator.Send(new GetBoardByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBoardsForUser(int userId)
        {
            var result = await _mediator.Send(new GetBoardsForUserQuery(userId));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard(int id, UpdateBoardCommand command)
        {
            if (id != command.Id) return BadRequest();
            var updated = await _mediator.Send(command);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            await _mediator.Send(new DeleteBoardCommand(id));
            return NoContent();
        }

        [HttpPost("{boardId}/members")]
        public async Task<IActionResult> AddMember(int boardId, AddBoardMemberCommand command)
        {
            if (boardId != command.BoardId) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{boardId}/members/{userId}")]
        public async Task<IActionResult> RemoveMember(int boardId, int userId)
        {
            await _mediator.Send(new RemoveBoardMemberCommand(boardId, userId));
            return NoContent();
        }

        [HttpGet("dashboard/{userId}")]
        public async Task<IActionResult> GetDashboardBoardsWithOwners(int userId)
        {
            var result = await _mediator.Send(new GetDashboardBoardsWithOwnersQuery(userId));
            return Ok(result);
        }
        [HttpGet("detailed/{boardId}")]
        public async Task<IActionResult> GetDetailedBoardById(int boardId)
        {
            var result = await _mediator.Send(new GetDetailedBoardByIdQuery(boardId));
            return Ok(result);
        }
    }
}
