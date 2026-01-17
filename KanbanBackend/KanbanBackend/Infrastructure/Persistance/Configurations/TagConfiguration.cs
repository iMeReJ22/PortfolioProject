using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(e => e.Id).HasName("Tags_pk");

            builder.HasIndex(e => e.BoardId, "TagBoardIdIndex");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.ColorHex).HasMaxLength(7);
            builder.Property(e => e.CreatedAt).HasColumnType("datetime");
            builder.Property(e => e.Name).HasMaxLength(50);

            builder.HasOne(d => d.Board).WithMany(p => p.Tags)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tags_Boards");
        }
    }
}
