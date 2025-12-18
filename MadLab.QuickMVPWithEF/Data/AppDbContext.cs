using Microsoft.EntityFrameworkCore;
using MadLab.QuickMVPWithEF.Entities;

namespace MadLab.QuickMVPWithEF.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoList>()
                .HasMany(t => t.Items)
                .WithOne(i => i.TodoList)
                .HasForeignKey(i => i.TodoListId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
