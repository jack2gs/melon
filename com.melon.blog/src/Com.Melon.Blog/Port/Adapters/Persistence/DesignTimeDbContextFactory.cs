using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Com.Melon.Blog.Port.Adapters.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .AddEnvironmentVariables()
//                .Build();
//            
//             builder.AddEnvironmentVariables();
            
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            builder.UseSqlServer("server=.;database=Blog;User Id=sa;Password=Tester99;");
            return new BlogDbContext(builder.Options);
        }
    }
}
