using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using KanbanBackend.Infrastructure.Services.HandleRecursiveDelete;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.DeleteBoard
{
    public class DeleteBoardCommandHandler
    : IRequestHandler<DeleteBoardCommand, Unit>
    {
        private readonly IBoardRepository _boards;
        private readonly IHandleRecursiveDeleteService _recursive;

        public DeleteBoardCommandHandler(IBoardRepository boards, IHandleRecursiveDeleteService recursive)
        {
            _boards = boards;
            _recursive = recursive;
        }

        public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken ct)
        {
            var board = await _boards.GetByIdAsync(request.Id);
            if (board == null)
                throw new NotFoundException("Board", request.Id);

            await _recursive.HandleBoardAsync(board);

            return Unit.Value;
        }
    }

}
