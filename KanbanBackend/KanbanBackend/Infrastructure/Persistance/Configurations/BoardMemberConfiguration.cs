using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanBackend.Infrastructure.Persistance.Configurations
{
    public class BoardMemberConfiguration : IEntityTypeConfiguration<BoardMember>
    {
        public void Configure(EntityTypeBuilder<BoardMember> builder)
        {
            builder.HasKey(e => new { e.UserId, e.BoardId }).HasName("BoardMembers_pk");

            builder.Property(e => e.Role).HasMaxLength(50);

            builder.HasOne(d => d.Board).WithMany(p => p.BoardMembers)
                .HasForeignKey(d => d.BoardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BoardMemebers_Boards");

            builder.HasOne(d => d.User).WithMany(p => p.BoardMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("BoardMemebers_Users");
        }
    }
}
