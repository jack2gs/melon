using Com.Melon.IdentityAccess.Domain;
using Microsoft.EntityFrameworkCore;

namespace Com.Melon.IdentityAccess.Port.Adapter.Persistance
{
    public class IdentityAccessDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public IdentityAccessDbContext(DbContextOptions<IdentityAccessDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Timestamp)
                    .IsRowVersion();

                entity.OwnsOne(o => o.Email).Property(x=>x.EmailAddress).HasMaxLength(50);
                entity.OwnsOne(o => o.Password).Property(x => x.PasswordString).HasMaxLength(20);
            });
        }
    }
}
