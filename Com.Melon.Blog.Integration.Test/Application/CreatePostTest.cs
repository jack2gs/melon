using System;
using System.Linq;
using System.Threading;
using Com.Melon.Blog.Application;
using Com.Melon.Blog.Domain;
using Com.Melon.Blog.Integration.Test.Port.Adapters.Persistence;
using FluentAssertions;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Blog.Integration.Test.Application
{
    public class CreatePostTest : Specification, IClassFixture<BlogDbFixture>
    {
        protected BlogDbFixture BlogDbFixture;

        protected CreatePostCommandHandler CreatePostCommandHandler;

        protected CreatePostCommand CreatePostCommand;

        protected Exception Exception;

        protected override void EstablishContext()
        {
            BlogDbFixture = new BlogDbFixture();
            CreatePostCommandHandler = new CreatePostCommandHandler(BlogDbFixture.PostRepository);
            CreatePostCommand = BuildPostCommand();

        }

        protected virtual CreatePostCommand BuildPostCommand()
        {
            throw new NotImplementedException();
        }

        protected async override void Because()
        {
            Exception = await Record.ExceptionAsync(() => CreatePostCommandHandler.Handle(CreatePostCommand, default));
        }
    }

    public class When_post_is_valid : CreatePostTest
    {
        protected override CreatePostCommand BuildPostCommand()
        {
            return new CreatePostCommand("My blog", "Hello world");
        }

        [Observation]
        void should_save_it_into_db()
        {
            BlogDbFixture.BlogDbContext.Posts.Count().Should().Be(2);
            Post post = BlogDbFixture.BlogDbContext.Posts.ToList()[1];
            CreatePostCommand postCommand = BuildPostCommand();
            post.Title.Should().Be(postCommand.Title);
            post.Content.Should().Be(postCommand.Content);
        }
    }

    public class When_title_is_empty : CreatePostTest
    {
        protected override CreatePostCommand BuildPostCommand()
        {
            return new CreatePostCommand("", "my content");
        }

        [Observation]
        void should_throw_exception()
        {
            ArgumentException exception = Exception as ArgumentException;
            exception.Message.Should().Be("Please set the title of the post.");
        }
    }
}
