using Com.Melon.Blog.Application;
using Com.Melon.Blog.Domain;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Blog.Unit.Test.Application
{
    public class PostQueryHandlerTest: Specification
    {
        protected PostQuery PostQuery;

        protected PostQueryHandler PostQueryHandler;

        protected PostData ExpectedPostData;

        protected PostData ActualPostData;

        protected IPostRepository PostRepositoryMock;

        protected Exception ActualException;

        protected override void EstablishContext()
        {
            PostRepositoryMock = Mock.Of<IPostRepository>();
            PostQueryHandler = new PostQueryHandler(PostRepositoryMock);
        }

        protected async override void Because()
        {
           ActualException = await Record.ExceptionAsync(async ()=> ActualPostData = await PostQueryHandler.Handle(PostQuery, default(CancellationToken)));
        }
    }

    public class When_post_exists: PostQueryHandlerTest
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();

            ExpectedPostData = new PostData(1, "PostTitle", "PostConent");
            PostQuery = new PostQuery(ExpectedPostData.PostId);
            Mock.Get(PostRepositoryMock)
                .Setup(x => x.GetById(It.Is<int>(y => y == ExpectedPostData.PostId)))
                .Returns(new Post(ExpectedPostData.PostId, ExpectedPostData.Title, ExpectedPostData.Content));
        }

        [Observation]
        void should_return_the_post()
        {
            ActualPostData.Should().BeEquivalentTo(ExpectedPostData);
        }
    }

    public class When_post_not_exists : PostQueryHandlerTest
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();

            ExpectedPostData = null;
            PostQuery = new PostQuery(1);
            Mock.Get(PostRepositoryMock)
                .Setup(x => x.GetById(It.Is<int>(y => y == PostQuery.PostId)))
                .Returns<Post>(null);
        }

        [Observation]
        void should_return_the_post()
        {
            ActualPostData.Should().BeNull();
        }

        [Observation]
        void should_not_throw_exception()
        {
            ActualException.Should().BeNull();
        }
    }
}
