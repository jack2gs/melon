using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Com.Melon.IdentityAccess.Port.Adapter.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<IdentityAccessDbContext>
    {
            public IdentityAccessDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<IdentityAccessDbContext>();
                builder.UseSqlServer("server=.;database=Identity;trusted_connection=true;");
                return new IdentityAccessDbContext(builder.Options);
            }
    }
}
