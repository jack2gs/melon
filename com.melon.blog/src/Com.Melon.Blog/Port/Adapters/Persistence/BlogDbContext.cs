using Com.Melon.Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Com.Melon.Blog.Port.Adapters.Persistence
{
    public class BlogDbContext: DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Content).HasMaxLength(5000);
                entity.Property(e => e.DateTimeCreated).IsRequired();
                entity.Property(e => e.DateTimeLastModified).IsRequired();
                entity.Property(e => e.Timestamp).IsRowVersion();
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
