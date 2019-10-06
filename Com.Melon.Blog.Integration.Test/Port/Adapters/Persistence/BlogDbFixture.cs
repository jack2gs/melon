using Com.Melon.Blog.Domain;
using Com.Melon.Blog.Port.Adapters.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Com.Melon.Blog.Integration.Test.Port.Adapters.Persistence
{
    public class BlogDbFixture : IDisposable
    {
        public BlogDbContext BlogDbContext { get; }

        public IPostRepository PostRepository { get; }

        public BlogDbFixture()
        {
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            builder.UseSqlServer("server=.;database=Blog;user id=sa;password=Tester99;");
            BlogDbContext = new BlogDbContext(builder.Options);
            PostRepository = new PostRepository(BlogDbContext);

            BlogDbContext.Database.EnsureDeleted();
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