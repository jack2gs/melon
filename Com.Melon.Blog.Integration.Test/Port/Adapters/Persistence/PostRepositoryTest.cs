using Com.Melon.Blog.Domain;
using Com.Melon.Blog.Port.Adapters.Persistence;
using FluentAssertions;
using System;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Blog.Integration.Test
{
    public class When_save_post : Specification, IClassFixture<BlogDbContextFixture>
    {
        protected IPostRepository PostRepository;

        protected BlogDbContextFixture BlogDbContextFixture;

        public When_save_post()
        {
            this.BlogDbContextFixture = new BlogDbContextFixture();
        }

        protected override void EstablishContext()
        {
            PostRepository = new PostRepository(BlogDbContextFixture.BlogDbContext);

        }

        [Observation]
        public void should_save_post()
        {
            string title = "FakedTitle";
            string content = "FakedContent";
            Post post = new Post(title, content);

            PostRepository.Save(post);
            Post postFromDb = PostRepository.GetById(post.Id);

            postFromDb.Title.Should().Be(title);
            postFromDb.Content.Should().Be(content);
        }
    }
}
