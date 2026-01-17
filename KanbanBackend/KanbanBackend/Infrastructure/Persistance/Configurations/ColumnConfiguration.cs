using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class ColumnConfiguration : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.HasKey(e => e.Id).HasName("Columns_pk");

            builder.HasIndex(e => e.BoardId, "ColBoardIdIndex");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.Name).HasMaxLength(100);

            builder.HasOne(d => d.Board).WithMany(p => p.Columns)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Columns_Boards");
        }
    }
}
