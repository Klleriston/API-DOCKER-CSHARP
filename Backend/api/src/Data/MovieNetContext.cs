using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Data
{
    public class MovieNetContext : DbContext
    {
        public MovieNetContext(DbContextOptions options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
