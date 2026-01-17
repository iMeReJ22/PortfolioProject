using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
    {
        public void Configure(EntityTypeBuilder<TaskComment> builder)
        {
            builder.HasKey(e => e.Id).HasName("TaskComments_pk");

            builder.HasIndex(e => e.TaskId, "CommentTaskIdIndex");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");

            builder.HasOne(d => d.Author).WithMany(p => p.TaskComments)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("TaskComments_Users");

            builder.HasOne(d => d.Task).WithMany(p => p.TaskComments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TaskComments_Tasks");
        }
    }
}
