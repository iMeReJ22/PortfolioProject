using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id).HasName("Users_pk");

            builder.HasIndex(e => e.Email, "EmailIndex").IsUnique();

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");
            builder.Property(e => e.DisplayName).HasMaxLength(100);
            builder.Property(e => e.Email).HasMaxLength(256);
            builder.Property(e => e.PasswordHash).HasMaxLength(512);
        }
    }
}
