using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class TaskTypeConfiguration : IEntityTypeConfiguration<TaskType>
    {
        public void Configure(EntityTypeBuilder<TaskType> builder)
        {
            builder.HasKey(e => e.Id).HasName("TaskTypes_pk");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.ColorHex).HasMaxLength(7);
            builder.Property(e => e.Name).HasMaxLength(100);
        }
    }
}
