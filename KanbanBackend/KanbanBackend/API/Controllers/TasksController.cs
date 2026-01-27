using KanbanBackend.Application.Tasks.Commands.CreateTask;
using KanbanBackend.Application.Tasks.Commands.UpdateTask;
using KanbanBackend.Application.Tasks.Commands.DeleteTask;
using KanbanBackend.Application.Tasks.Commands.ReorderTasks;
using KanbanBackend.Application.Tasks.Commands.MoveTask;
using KanbanBackend.Application.Tasks.Commands.AssignTagToTask;
using KanbanBackend.Application.Tasks.Commands.RemoveTagFromTask;
using KanbanBackend.Application.Tasks.Queries.GetTasksForColumn;
using KanbanBackend.Application.Tasks.Queries.GetTasksForBoard;
using KanbanBackend.Application.Tasks.Queries.GetTaskById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KanbanBackend.Application.Tasks.Queries.GetTaskTypes;

namespace KanbanBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskCommand command)
        {
            var dto = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTaskById), new { id = dto.Id }, dto);
        }

        [HttpGet("column/{columnId}")]
        public async Task<IActionResult> GetTasksForColumn(int columnId)
        {
            var result = await _mediator.Send(new GetTasksForColumnQuery(columnId));
            return Ok(result);
        }

        [HttpGet("board/{boardId}")]
        public async Task<IActionResult> GetTasksForBoard(int boardId)
        {
            var result = await _mediator.Send(new GetTasksForBoardQuery(boardId));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskCommand command)
        {
            if (id != command.Id) return BadRequest();
            var updated = await _mediator.Send(command);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _mediator.Send(new DeleteTaskCommand(id));
            return NoContent();
        }

        [HttpPost("column/{columnId}/reorder")]
        public async Task<IActionResult> ReorderTasks(int columnId, ReorderTasksCommand command)
        {
            if (columnId != command.ColumnId) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("move")]
        public async Task<IActionResult> MoveTask(MoveTaskCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{taskId}/tags")]
        public async Task<IActionResult> AssignTagToTask(int taskId, AssignTagToTaskCommand command)
        {
            if (taskId != command.TaskId) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{taskId}/tags/{tagId}")]
        public async Task<IActionResult> RemoveTagFromTask(int taskId, int tagId)
        {
            await _mediator.Send(new RemoveTagFromTaskCommand(taskId, tagId));
            return NoContent();
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetTaskTypes()
        {
            var result = await _mediator.Send(new GetTaskTypesQuery());
            return Ok(result);
        }
    }
}
