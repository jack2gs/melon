using Com.Melon.Blog.Domain;
using Com.Melon.Blog.Port.Adapters.Persistence;
using FluentAssertions;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Blog.Integration.Test.Port.Adapters.Persistence
{
    public class PostRepositoryTest : Specification, IClassFixture<BlogDbFixture>
    {
        protected IPostRepository PostRepository;

        protected BlogDbFixture BlogDbContextFixture;

        public PostRepositoryTest()
        {
            this.BlogDbContextFixture = new BlogDbFixture();
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
