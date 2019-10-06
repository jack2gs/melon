using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Blog.Domain;
using Com.Melon.Blog.Port.Adapters.Persistence;
using Com.Melon.Core.Framework.Pagination;
using FluentAssertions;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Blog.Integration.Test.Port.Adapters.Persistence
{
    public class PostRepositoryTest : Specification, IClassFixture<BlogDbFixture>
    {
        protected IPostRepository PostRepository;

        protected readonly BlogDbFixture BlogDbContextFixture;

        public PostRepositoryTest()
        {
            this.BlogDbContextFixture = new BlogDbFixture();
        }

        protected override void EstablishContext()
        {
            PostRepository = new PostRepository(BlogDbContextFixture.BlogDbContext);
        }
    }

    public class When_save_post : PostRepositoryTest
    {
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

    public class When_get_all_posts : PostRepositoryTest
    {
        protected PagedResult<Post> ActualResults;

        protected override async Task BecauseAsync()
        {
            ActualResults = await PostRepository.GetAllAsync(1, 20, default(CancellationToken));
        }

        [Observation]
        void should_get_all_posts_with_pagination()
        {
            ActualResults.TotalItemsCount.Should().Be(1);
            ActualResults.ItemsCount.Should().Be(1);
        }
    }

    public class When_update_post : PostRepositoryTest
    {
        protected PagedResult<Post> ActualResults;
        protected Post Post;

        [Observation]
        async Task should_change_title_and_content()
        {
            string title = "FakedTitle";
            string content = "FakedContent";
            Post post = new Post(title, content);
            PostRepository.Save(post);
            
            Post postFromDb = PostRepository.GetById(post.Id);
            postFromDb.Update("PostTitle", "PostContent");
            await PostRepository.UpdateAsync(post);
            postFromDb = PostRepository.GetById(post.Id);
            postFromDb.Title.Should().Be("PostTitle");
            postFromDb.Content.Should().Be("PostContent");
        }
    }
}