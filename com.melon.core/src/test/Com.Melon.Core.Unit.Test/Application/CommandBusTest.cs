using Com.Melon.Core.Application;
using MediatR;
using Moq;
using System.Threading;
using XunitExtensions;

namespace Com.Melon.Core.Unit.Test.Application
{
    public class Given_a_command_bus: Specification
    {
        protected CommandBus CommandBus;

        protected DummyCommand ExpectedCommand;

        protected DummyCommandWithResult ExpectedCommandWithResult;

        protected Mock<IMediator> MediatorMock;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            ExpectedCommand = new DummyCommand();
            ExpectedCommandWithResult = new DummyCommandWithResult();
            MediatorMock = new Mock<IMediator>();
            CommandBus = new CommandBus(MediatorMock.Object);
        }

        public class When_send_the_command: Given_a_command_bus
        {
            protected override void Because()
            {
                CommandBus.Send(ExpectedCommand);
            }

            [Observation]
            void should_call_mediator()
            {
                MediatorMock.Verify(x => x.Send(ExpectedCommand, It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        public class When_send_the_command_synchroizedly_without_token : Given_a_command_bus
        {
            protected async override void Because()
            {
                await CommandBus.AsyncSend(ExpectedCommand);
            }

            [Observation]
            void should_call_mediator()
            {
                MediatorMock.Verify(x => x.Send(ExpectedCommand, It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        public class When_send_the_command_with_return_value : Given_a_command_bus
        {
            protected override void Because()
            {
                CommandBus.Send(ExpectedCommandWithResult);
            }

            [Observation]
            void should_call_mediator()
            {
                MediatorMock.Verify(x => x.Send(ExpectedCommandWithResult, It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        public class When_send_the_command_with_return_value_asynchronizedly : Given_a_command_bus
        {
            protected override void Because()
            {
                CommandBus.AsyncSend(ExpectedCommandWithResult);
            }

            [Observation]
            void should_call_mediator()
            {
                MediatorMock.Verify(x => x.Send(ExpectedCommandWithResult, It.IsAny<CancellationToken>()), Times.Once);
            }
        }
    }
}
