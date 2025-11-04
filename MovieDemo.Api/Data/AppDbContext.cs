using Microsoft.EntityFrameworkCore;
using MovieDemo.Api.Models;

namespace MovieDemo.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies => Set<Movie>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasIndex(m => new { m.Title, m.Year })
                .IsUnique();
        }

    }
}
