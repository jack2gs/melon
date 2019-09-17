using Com.Melon.Blog.Domain;
using Com.Melon.Blog.Port.Adapters.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Com.Melon.Blog.Integration.Test
{
    public class BlogDbContextFixture : IDisposable
    {
        public BlogDbContext BlogDbContext { get; }

        public BlogDbContextFixture()
        {
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            builder.UseSqlServer("server=.;database=Blog;trusted_connection=true;");
            BlogDbContext = new BlogDbContext(builder.Options);

            BlogDbContext.Database.Migrate();

            SeedPost();
        }

        private void SeedPost()
        {
            BlogDbContext.Posts.Add(new Post("Hello World", "This is my first post."));
            BlogDbContext.SaveChanges();
        }

        public void Dispose()
        {
            BlogDbContext.Database.EnsureDeleted();
        }
    }
}