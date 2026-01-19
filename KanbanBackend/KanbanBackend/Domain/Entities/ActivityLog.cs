using KanbanBackend.Domain.Entities;
using AsyncTask = System.Threading.Tasks.Task;

namespace KanbanBackend.Domain.Entities;

public partial class ActivityLog
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int BoardId { get; set; }

    public int? ActivityAuthorId { get; set; }

    public int? TagId { get; set; }

    public int? ColumnId { get; set; }

    public int? TaskId { get; set; }

    public int? TaskCommentId { get; set; }

    public int? MemberId { get; set; }

    public virtual User? ActivityAuthor { get; set; }

    public virtual Board Board { get; set; } = null!;

    public virtual Column? Column { get; set; }

    public virtual User? Member { get; set; }

    public virtual Tag? Tag { get; set; }

    public virtual Task? Task { get; set; }

    public virtual TaskComment? TaskComment { get; set; }
}
