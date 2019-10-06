using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Com.Melon.Blog.Application;
using Com.Melon.Blog.Domain;
using Com.Melon.Blog.Integration.Test.Port.Adapters.Persistence;
using FluentAssertions;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Blog.Integration.Test.Application
{
    public class UpdatePostTest: Specification, IClassFixture<BlogDbFixture>
    {
        protected BlogDbFixture BlogDbFixture { get; }

        protected IPostRepository PostRepository { get; }

        protected UpdatePostCommand UpdatePostCommand { get; set; }

        protected Exception Exception { get; set; }

        protected UpdatePostCommandHandler UpdatePostCommandHandler;

        public UpdatePostTest()
        {
            BlogDbFixture = new BlogDbFixture();

            PostRepository = BlogDbFixture.PostRepository;
        }

        protected override void EstablishContext()
        {
            UpdatePostCommand = GetUpdatePostCommand();
            UpdatePostCommandHandler = new UpdatePostCommandHandler(PostRepository);
        }

        protected virtual UpdatePostCommand GetUpdatePostCommand()
        {
            var post = BlogDbFixture.BlogDbContext.Posts.First();

            return new UpdatePostCommand(post.Id, "ChangedTitle", "ChangedContent");
        }

        protected override async Task BecauseAsync()
        {
            Exception = await Record.ExceptionAsync(() => UpdatePostCommandHandler.Handle(UpdatePostCommand, default(CancellationToken)));
        }
    }

    public class When_update_post: UpdatePostTest
    {
        [Observation]
        void should_update_post()
        {
            var post = PostRepository.GetById(UpdatePostCommand.PostId);

            post.Title.Should().Be(UpdatePostCommand.Title);
            post.Content.Should().Be(UpdatePostCommand.Content);
        }
    }
}
