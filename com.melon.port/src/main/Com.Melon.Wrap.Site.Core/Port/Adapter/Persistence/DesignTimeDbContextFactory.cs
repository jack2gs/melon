using Com.Melon.Wrap.Site.Core.Port.Adapter.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Com.Melon.IdentityAccess.Port.Adapter.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WrapSiteCoreDbContext>
    {
            public WrapSiteCoreDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<WrapSiteCoreDbContext>();
                builder.UseSqlServer("server=.;database=Web;trusted_connection=true;");
                return new WrapSiteCoreDbContext(builder.Options);
            }
    }
}
