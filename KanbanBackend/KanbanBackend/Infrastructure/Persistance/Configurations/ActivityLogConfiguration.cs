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

            builder.HasIndex(e => e.ActivityTypeId, "ActivityTypeIdIndex");

            builder.HasIndex(e => e.UserId, "UserIdIndex");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");
            builder.Property(e => e.Description).HasMaxLength(500);

            builder.HasOne(d => d.ActivityType).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ActivityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActivityLog_ActivityType");

            builder.HasOne(d => d.Board).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ActivityLog_Boards");

            builder.HasOne(d => d.Column).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ActivityLog_Columns");

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

            builder.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ActivityLog_Users");
        }
    }
}
