using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> builder)
        {
            builder.HasKey(e => e.Id).HasName("ActivityTypes_pk");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.DisplayName).HasMaxLength(100);
            builder.Property(e => e.Name).HasMaxLength(50);
        }
    }
}
