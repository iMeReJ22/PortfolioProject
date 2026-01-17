using KanbanBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Infrastructure.Persistance
{
    public class KanbanDbContext : DbContext
    {
        public KanbanDbContext(DbContextOptions<KanbanDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

        public virtual DbSet<ActivityType> ActivityTypes { get; set; }

        public virtual DbSet<Board> Boards { get; set; }

        public virtual DbSet<BoardMember> BoardMembers { get; set; }

        public virtual DbSet<Column> Columns { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Domain.Entities.Task> Tasks { get; set; }

        public virtual DbSet<TaskComment> TaskComments { get; set; }

        public virtual DbSet<TaskType> TaskTypes { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KanbanDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
