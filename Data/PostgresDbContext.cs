using Microsoft.EntityFrameworkCore;

using SampelToDo.Models;

namespace SampelToDo.Data
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        {
        }
        public DbSet<TodoModel> Todos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoModel>().ToTable("Todo");
        }
    }
    
}
