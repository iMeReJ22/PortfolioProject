using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.HasKey(e => e.Id).HasName("ActivityLog_pk");

            builder.ToTable("ActivityLog");

            builder.HasIndex(e => e.BoardId, "ALBoardIdIndex");

            builder.HasIndex(e => e.ActivityAuthorId, "UserIdIndex");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.Name).HasMaxLength(50);

            builder.HasOne(d => d.ActivityAuthor).WithMany(p => p.ActivityLogActivityAuthors)
                .HasForeignKey(d => d.ActivityAuthorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ActivityLog_Users_Author");

            builder.HasOne(d => d.Board).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActivityLog_Boards");

            builder.HasOne(d => d.Column).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ActivityLog_Columns");

            builder.HasOne(d => d.Member).WithMany(p => p.ActivityLogMembers)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("ActivityLog_Users_Member");

            builder.HasOne(d => d.Tag).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ActivityLog_Tags");

            builder.HasOne(d => d.TaskComment).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.TaskCommentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ActivityLog_TaskComments");

            builder.HasOne(d => d.Task).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ActivityLog_Tasks");
        }
    }
}
