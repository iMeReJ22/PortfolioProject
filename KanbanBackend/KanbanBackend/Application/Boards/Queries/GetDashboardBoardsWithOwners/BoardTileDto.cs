using KanbanBackend.Application.Common.DTOs;
using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Application.Boards.Queries.GetDashboardBoardsWithOwners
{
    public class BoardTileDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required UserDto Owner { get; set; }
        public required IReadOnlyCollection<BoardMemberDto> BoardMembers { get; set; }
    }
}
