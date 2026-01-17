using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.RemoveBoard
{
    public class RemoveBoardMemberCommandHandler
    : IRequestHandler<RemoveBoardMemberCommand, Unit>
    {
        private readonly IBoardRepository _boards;

        public RemoveBoardMemberCommandHandler(IBoardRepository boards)
        {
            _boards = boards;
        }

        public async Task<Unit> Handle(RemoveBoardMemberCommand request, CancellationToken ct)
        {
            var member = new BoardMember
            {
                BoardId = request.BoardId,
                UserId = request.UserId,
                Role = ""
            };

            await _boards.RemoveMemberAsync(member);

            return Unit.Value;
        }
    }

}
