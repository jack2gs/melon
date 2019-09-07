using Com.Melon.Wrap.Site.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Com.Melon.Wrap.Site.Core.Port.Adapter.Persistence
{
    public class WrapSiteCoreDbContext: DbContext
    {
        public DbSet<Session> Sessions { get; set; }

        public WrapSiteCoreDbContext(DbContextOptions<WrapSiteCoreDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.SessionToken).HasMaxLength(256);
                entity.Property(x => x.Timestamp).IsRowVersion();
            });
        }
    }
}
