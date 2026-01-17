using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Exceptions;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.UpdateBoard
{
    public class UpdateBoardCommandHandler
    : IRequestHandler<UpdateBoardCommand, BoardDto>
    {
        private readonly IBoardRepository _boards;
        private readonly IMapper _mapper;

        public UpdateBoardCommandHandler(IBoardRepository boards, IMapper mapper)
        {
            _boards = boards;
            _mapper = mapper;
        }

        public async Task<BoardDto> Handle(UpdateBoardCommand request, CancellationToken ct)
        {
            var board = await _boards.GetByIdAsync(request.Id);
            if (board == null)
                throw new NotFoundException("Board", request.Id);

            board.Name = request.Name;
            board.Description = request.Description;

            await _boards.UpdateAsync(board);

            return _mapper.Map<BoardDto>(board);
        }
    }

}
