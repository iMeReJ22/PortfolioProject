using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.RemoveBoard
{
    public class RemoveBoardMemberCommandHandler
    : IRequestHandler<RemoveBoardMemberCommand, Unit>
    {
        private readonly IBoardRepository _boards;
        private readonly IActivityLoggerService _logger;
        public RemoveBoardMemberCommandHandler(IBoardRepository boards, IActivityLoggerService logger)
        {
            _boards = boards;
            _logger = logger;
        }

        public async Task<Unit> Handle(RemoveBoardMemberCommand request, CancellationToken ct)
        {
            var member = await _boards.GetMemberAsync(request.BoardId, request.UserId);
            if (member == null)
                throw new NotFoundException("BoardMember", request.UserId);
            await _logger.AddLogBoardMemberAsync("Board Member Removed", "removed from", request.BoardId, request.UserId);

            await _boards.RemoveMemberAsync(member);

            return Unit.Value;
        }
    }

}
