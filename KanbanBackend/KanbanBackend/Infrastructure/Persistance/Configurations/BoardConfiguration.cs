using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasKey(e => e.Id).HasName("Boards_pk");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.Name).HasMaxLength(100);

            builder.HasOne(d => d.Owner).WithMany(p => p.Boards)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Boards_Users");
        }
    }
}
