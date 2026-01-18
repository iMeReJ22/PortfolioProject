using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.DeleteBoard
{
    public class DeleteBoardCommandHandler
    : IRequestHandler<DeleteBoardCommand, Unit>
    {
        private readonly IBoardRepository _boards;
        private readonly IColumnRepository _columns;

        public DeleteBoardCommandHandler(IBoardRepository boards, IColumnRepository columns)
        {
            _boards = boards;
            _columns = columns;
        }

        public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken ct)
        {
            var board = await _boards.GetByIdAsync(request.Id);
            if (board == null)
                throw new NotFoundException("Board", request.Id);

            var columns = await _columns.GetForBoardAsync(board.Id);

            await _boards.RemoveMemberAsync(new BoardMember { BoardId = board.Id, Role = "", UserId = board.OwnerId });
            await _boards.DeleteAsync(board);

            return Unit.Value;
        }
    }

}
