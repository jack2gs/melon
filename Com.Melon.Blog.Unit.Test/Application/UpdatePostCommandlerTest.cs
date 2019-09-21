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
    public class UpdatePostCommandlerTest: Specification
    {
        protected UpdatePostCommand UpdatePostCommand;

        protected UpdatePostCommandHandler UpdatePostCommandHandler;

        protected IPostRepository PostRepositoryMock;

        protected Exception AcutalException;

        protected override void EstablishContext()
        {
            UpdatePostCommand = CreateUpdatePostCommand();
            PostRepositoryMock = Mock.Of<IPostRepository>();
            UpdatePostCommandHandler = new UpdatePostCommandHandler(PostRepositoryMock);
        }

        protected virtual UpdatePostCommand CreateUpdatePostCommand()
        {
            throw new NotImplementedException();
        }

        protected async override void Because()
        {
            AcutalException = await Record.ExceptionAsync(() => UpdatePostCommandHandler.Handle(UpdatePostCommand, default(CancellationToken)));
            
        }
    }

    public class When_update_the_post: UpdatePostCommandlerTest
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Mock.Get(PostRepositoryMock).Setup(x => x.GetById(It.Is<int>(y => y == 1))).Returns(new Post(1, "Title", "Content"));
        }

        protected override UpdatePostCommand CreateUpdatePostCommand()
        {
            return new UpdatePostCommand(1, "FakedTitle", "FakedContent");
        }

        [Observation]
        void should_save_into_the_database()
        {
            Mock.Get(PostRepositoryMock).Verify(x => x.Save(It.Is<Post>(y => y.Id == UpdatePostCommand.PostId && y.Title == UpdatePostCommand.Title && y.Content == UpdatePostCommand.Content)), Times.Once);
        }

        [Observation]
        void should_not_throw_exception()
        {
            AcutalException.Should().BeNull();
        }
    }

    public class When_post_not_exist : UpdatePostCommandlerTest
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Mock.Get(PostRepositoryMock).Setup(x => x.GetById(It.Is<int>(y => y == 1))).Returns<Post>(null);
        }

        protected override UpdatePostCommand CreateUpdatePostCommand()
        {
            return new UpdatePostCommand(1, "FakedTitle", "FakedContent");
        }

        [Observation]
        void should_not_save_into_the_database()
        {
            Mock.Get(PostRepositoryMock).Verify(x => x.Save(It.Is<Post>(y => y.Id == UpdatePostCommand.PostId)), Times.Never);
        }

        [Observation]
        void should_throw_exception()
        {
            AcutalException.Should().NotBeNull();
            ArgumentException exception = AcutalException as ArgumentException;
            exception.Message.Should().Be("The post doesn't exist.");
        }
    }
}
