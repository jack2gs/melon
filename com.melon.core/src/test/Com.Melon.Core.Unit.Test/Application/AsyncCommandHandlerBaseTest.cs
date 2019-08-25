using Com.Melon.Core.Application;
using FluentAssertions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using XunitExtensions;

namespace Com.Melon.Core.Unit.Test.Application
{
    public class Given_a_async_commandhandlerbase : Specification
    {
        protected DummyImplementationCommandHandler UnderTest;

        protected DummyCommand ExpectedCommand;

        protected CancellationToken CancellationToken;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            UnderTest = new DummyImplementationCommandHandler();
            ExpectedCommand = new DummyCommand();
        }

        protected class DummyImplementationCommandHandler : AsyncCommandHandlerBase<DummyCommand>
        {
            public DummyCommand ReceievedCommand { get; private set; }

            public bool IsExecuteCalled { get; private set; }

            public override Task Execute(DummyCommand command, CancellationToken token)
            {
                ReceievedCommand = command;

                IsExecuteCalled = true;

                return Task.CompletedTask;
            }
        }

        protected class DummyCommand : CommandBase
        {

        }

        public class When_conrete : Given_a_async_commandhandlerbase
        {
            [Observation]
            void should_be_assignable_to_ICommandHandler()
            {
                UnderTest.Should().BeAssignableTo<IAsyncCommandHandler<DummyCommand>>();
            }

            [Observation]
            void should_be_assignable_to_IRequestHandler()
            {
                UnderTest.Should().BeAssignableTo<IRequestHandler<DummyCommand>>();
            }
        }

        public class When_call_execute : Given_a_async_commandhandlerbase
        {
            protected Task ExpectedReturn;

            protected Task ActualReturn;
            protected override void EstablishContext()
            {
                base.EstablishContext();
                ExpectedReturn = Task.CompletedTask;
            }

            protected override void Because()
            {
                ActualReturn = UnderTest.Execute(ExpectedCommand, CancellationToken);
            }

            [Observation]
            void should_receieve_the_command()
            {
                UnderTest.ReceievedCommand.Should().Be(ExpectedCommand);
            }

            [Observation]
            void should_receieve_the_task()
            {
                ActualReturn.Should().Be(ExpectedReturn);
            }
        }

        public class When_call_handler : Given_a_async_commandhandlerbase
        {
            protected MediatR.Unit ExpectedReturn;

            protected MediatR.Unit ActualReturn;

            protected override void EstablishContext()
            {
                base.EstablishContext();
                ExpectedReturn = MediatR.Unit.Value; ;
            }

            protected async override void Because()
            {
                ActualReturn = await (UnderTest as IRequestHandler<DummyCommand>).Handle(ExpectedCommand, CancellationToken);
            }

            [Observation]
            void should_receieve_the_command()
            {
                UnderTest.ReceievedCommand.Should().Be(ExpectedCommand);
            }

            [Observation]
            void should_receieve_the_task()
            {
                ActualReturn.Should().Be(ExpectedReturn);
            }

            [Observation]
            void should_call_execute()
            {
                UnderTest.IsExecuteCalled.Should().BeTrue();
            }
        }
    }
}
