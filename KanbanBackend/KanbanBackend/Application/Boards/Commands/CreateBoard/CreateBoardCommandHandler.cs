using AutoMapper;
using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Application.Common.Interfaces;
using KanbanBackend.Domain.Entities;
using MediatR;

namespace KanbanBackend.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandHandler
    : IRequestHandler<CreateBoardCommand, BoardDto>
    {
        private readonly IBoardRepository _boards;
        private readonly IMapper _mapper;

        public CreateBoardCommandHandler(IBoardRepository boards, IMapper mapper)
        {
            _boards = boards;
            _mapper = mapper;
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
            
            await _boards.AddMemberAsync(new BoardMember { BoardId = board.Id, UserId = board.OwnerId, Role = "Owner" });

            return _mapper.Map<BoardDto>(board);
        }
    }

}
