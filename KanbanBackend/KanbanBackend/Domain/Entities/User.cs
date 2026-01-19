using KanbanBackend.Domain.Entities;
using System;
using System.Collections.Generic;

namespace KanbanBackend.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogActivityAuthors { get; set; } = new List<ActivityLog>();

    public virtual ICollection<ActivityLog> ActivityLogMembers { get; set; } = new List<ActivityLog>();

    public virtual ICollection<BoardMember> BoardMembers { get; set; } = new List<BoardMember>();

    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();

    public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
