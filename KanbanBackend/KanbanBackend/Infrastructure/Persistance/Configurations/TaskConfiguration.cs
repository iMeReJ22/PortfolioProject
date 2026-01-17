using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.HasKey(e => e.Id).HasName("Tasks_pk");

            builder.HasIndex(e => e.ColumnId, "ColumnIdIndex");

            builder.HasIndex(e => e.OrderIndex, "OrderIndexIndex");

            builder.HasIndex(e => e.TaskTypeId, "TaskTypeIdIndex");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");
            builder.Property(e => e.Title).HasMaxLength(200);

            builder.HasOne(d => d.Column).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tasks_Columns");

            builder.HasOne(d => d.CreatedByUser).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Tasks_Users");

            builder.HasOne(d => d.TaskType).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.TaskTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tasks_TaskType");

            builder.HasMany(d => d.Tags).WithMany(p => p.Tasks)
                .UsingEntity<Dictionary<string, object>>(
                    "TaskTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("TaskTags_Tags"),
                    l => l.HasOne<Domain.Entities.Task>().WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("TaskTags_Tasks"),
                    j =>
                    {
                        j.HasKey("TaskId", "TagId").HasName("TaskTags_pk");
                        j.ToTable("TaskTags");
                        j.HasIndex(new[] { "TagId" }, "TagIdIndex");
                        j.HasIndex(new[] { "TaskId" }, "TaskIdIndex");
                    });
        }
    }

}
