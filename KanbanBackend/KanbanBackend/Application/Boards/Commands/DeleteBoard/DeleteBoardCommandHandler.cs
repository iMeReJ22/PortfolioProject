using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.DeleteBoard
{
    public class DeleteBoardCommandHandler
    : IRequestHandler<DeleteBoardCommand, Unit>
    {
        private readonly IBoardRepository _boards;

        public DeleteBoardCommandHandler(IBoardRepository boards)
        {
            _boards = boards;
        }

        public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken ct)
        {
            var board = await _boards.GetByIdAsync(request.Id);
            if (board == null)
                throw new NotFoundException("Board", request.Id);

            await _boards.DeleteAsync(board);

            return Unit.Value;
        }
    }

}
