using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using KanbanBackend.Infrastructure.Services.ActivityLogger;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.UpdateBoard
{
    public class UpdateBoardCommandHandler
    : IRequestHandler<UpdateBoardCommand, BoardDto>
    {
        private readonly IBoardRepository _boards;
        private readonly IMapper _mapper;
        private readonly Infrastructure.Services.ActivityLogger.IActivityLoggerService _logger;

        public UpdateBoardCommandHandler(IBoardRepository boards, IMapper mapper, Infrastructure.Services.ActivityLogger.IActivityLoggerService logger)
        {
            _boards = boards;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BoardDto> Handle(UpdateBoardCommand request, CancellationToken ct)
        {
            var board = await _boards.GetByIdAsync(request.Id);
            if (board == null)
                throw new NotFoundException("Board", request.Id);

            board.Name = request.Name;
            board.Description = request.Description;

            await _boards.UpdateAsync(board);

            await _logger.AddLogBoardAsync("Board Updated", "updated", request.Id);

            return _mapper.Map<BoardDto>(board);
        }
    }

}
