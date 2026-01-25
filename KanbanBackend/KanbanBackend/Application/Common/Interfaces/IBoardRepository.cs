using KanbanBackend.Application.Boards.Queries.GetDashboardBoardsWithOwners;
using KanbanBackend.Domain.Entities;

namespace KanbanBackend.Application.Common.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board?> GetBoardAsync(int id);
        Task<IReadOnlyCollection<Board>> GetForUserAsync(int userId);
        Task<bool> ExistsAsync(int id);
        Task<bool> UserHasAccessAsync(int boardId, int userId);
        Task<Board?> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(Board board);
        System.Threading.Tasks.Task UpdateAsync(Board board);

        System.Threading.Tasks.Task DeleteAsync(Board board);

        Task<IReadOnlyCollection<BoardMember>> GetMembersAsync(int boardId);
        Task<BoardMember?> GetMemberAsync(int boardId, int memberId);
        System.Threading.Tasks.Task AddMemberAsync(BoardMember member);
        System.Threading.Tasks.Task RemoveMemberAsync(BoardMember member);
        System.Threading.Tasks.Task<int> GetMaxId();
        Task<IReadOnlyCollection<Board>> GetDashboardForUser(int userId);
        System.Threading.Tasks.Task RemoveMembersRangeAsync(IReadOnlyCollection<BoardMember> members);
    }
}
