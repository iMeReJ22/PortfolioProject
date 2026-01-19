using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandHandler
    : IRequestHandler<CreateBoardCommand, BoardDto>
    {
        private readonly IBoardRepository _boards;
        private readonly IMapper _mapper;
        private readonly Infrastructure.Services.ActivityLogger.IActivityLoggerService _logger;

        public CreateBoardCommandHandler(IBoardRepository boards, IMapper mapper, Infrastructure.Services.ActivityLogger.IActivityLoggerService logger)
        {
            _boards = boards;
            _mapper = mapper;
            _logger = logger;
        }



        public async Task<BoardDto> Handle(CreateBoardCommand request, CancellationToken ct)
        {
            var id = await _boards.GetMaxId();
            var board = new Board
            {
                Id = ++id,
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                OwnerId = request.OwnerId
            };

            await _boards.AddAsync(board);
            await _logger.AddLogBoardAsync("Board Created", "created", id);
            await _boards.AddMemberAsync(new BoardMember { BoardId = board.Id, UserId = board.OwnerId, Role = "Owner" });
            return _mapper.Map<BoardDto>(board);
        }
        
    }

}
