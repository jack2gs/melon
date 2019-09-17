using Com.Melon.Blog.Application;
using Com.Melon.Blog.Domain;
using Moq;
using System.Threading;
using XunitExtensions;

namespace Com.Melon.Blog.Unit.Test.Application
{
    public class CreatePostCommandHandlerTestBase : Specification
    {
        protected CreatePostCommandHandler UnderTest;

        protected CreatePostCommand Command;

        protected IPostRepository PostRepositoryMock;

        protected override void EstablishContext()
        {
            Command = new CreatePostCommand("Title", "Content");
            PostRepositoryMock = Mock.Of<IPostRepository>();

            SetupStubs();

            UnderTest = new CreatePostCommandHandler(PostRepositoryMock);
        }

        protected virtual void SetupStubs()
        {
        }

        protected override void Because()
        {
            UnderTest.Handle(Command, default(CancellationToken));
        }
    }

    public class When_creating_valid_post : CreatePostCommandHandlerTestBase
    {
       [Observation] 
       void should_save_it_into_repository()
        {
            Mock.Get(PostRepositoryMock).Verify(x => x.Save(It.Is<Post>(y=>y.Title==Command.Title&&y.Content==Command.Content)), Times.Once);
        }
    }
}
