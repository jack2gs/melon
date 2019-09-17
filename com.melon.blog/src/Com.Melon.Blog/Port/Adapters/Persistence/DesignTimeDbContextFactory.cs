using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Com.Melon.Blog.Port.Adapters.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            builder.UseSqlServer("server=.;database=Blog;trusted_connection=true;");
            return new BlogDbContext(builder.Options);
        }
    }
}
