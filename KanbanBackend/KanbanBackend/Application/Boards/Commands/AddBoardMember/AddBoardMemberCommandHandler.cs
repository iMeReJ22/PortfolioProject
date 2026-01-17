using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.AddBoardMember
{
    public class AddBoardMemberCommandHandler
    : IRequestHandler<AddBoardMemberCommand, Unit>
    {
        private readonly IBoardRepository _boards;

        public AddBoardMemberCommandHandler(IBoardRepository boards)
        {
            _boards = boards;
        }

        public async Task<Unit> Handle(AddBoardMemberCommand request, CancellationToken ct)
        {
            var member = new BoardMember
            {
                BoardId = request.BoardId,
                UserId = request.UserId,
                Role = request.Role
            };

            await _boards.AddMemberAsync(member);

            return Unit.Value;
        }
    }

}
