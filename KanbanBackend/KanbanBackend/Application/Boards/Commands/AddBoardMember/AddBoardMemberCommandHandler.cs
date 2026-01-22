using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.AddBoardMember
{
    public class AddBoardMemberCommandHandler
    : IRequestHandler<AddBoardMemberCommand, Unit>
    {
        private readonly IBoardRepository _boards;
        private readonly IActivityLoggerService _logger;

        public AddBoardMemberCommandHandler(IBoardRepository boards, IActivityLoggerService logger)
        {
            _boards = boards;
            _logger = logger;
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

            var dbMember = await _boards.GetMemberAsync(request.BoardId, request.UserId);
            if (dbMember == null)
                throw new NotFoundException("BoardMember", request.UserId);

            await _logger.AddLogBoardMemberAsync("Board Member Added", "added to", request.BoardId, request.UserId);

            return Unit.Value;
        }
    }

}
