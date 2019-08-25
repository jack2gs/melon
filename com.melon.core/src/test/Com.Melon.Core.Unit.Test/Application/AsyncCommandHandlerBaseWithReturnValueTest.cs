using Com.Melon.Core.Application;
using FluentAssertions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using XunitExtensions;

namespace Com.Melon.Core.Unit.Test.Application
{
    public class Given_a_async_commandhandlerbase_with_return_value : Specification
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

        protected class DummyImplementationCommandHandler : AsyncCommandHandlerBase<DummyCommand, DummyReturn>
        {
            public DummyCommand ReceievedCommand { get; private set; }

            public bool IsExecuteCalled { get; private set; }

            public Task<DummyReturn> DummyReturnResult { get; set; }

            public override Task<DummyReturn> Execute(DummyCommand command, CancellationToken token)
            {
                ReceievedCommand = command;

                IsExecuteCalled = true;

                return DummyReturnResult;
            }
        }

        protected class DummyCommand : CommandBase<DummyReturn>
        {

        }

        protected class DummyReturn
        {

        }

        public class When_conrete : Given_a_async_commandhandlerbase_with_return_value
        {
            [Observation]
            void should_be_assignable_to_ICommandHandler()
            {
                UnderTest.Should().BeAssignableTo<IAsyncCommandHandler<DummyCommand, DummyReturn>>();
            }

            [Observation]
            void should_be_assignable_to_IRequestHandler()
            {
                UnderTest.Should().BeAssignableTo<IRequestHandler<DummyCommand, DummyReturn>>();
            }
        }

        public class When_call_execute : Given_a_async_commandhandlerbase_with_return_value
        {
            protected Task<DummyReturn> ExpectedReturn;

            protected Task<DummyReturn> ActualReturn;

            protected override void EstablishContext()
            {
                base.EstablishContext();
                ExpectedReturn = Task.FromResult(new DummyReturn());
                UnderTest.DummyReturnResult = ExpectedReturn;
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

        public class When_call_handler : Given_a_async_commandhandlerbase_with_return_value
        {
            protected DummyReturn ExpectedReturn;

            protected DummyReturn ActualReturn;

            protected override void EstablishContext()
            {
                base.EstablishContext();
                ExpectedReturn = new DummyReturn();
                UnderTest.DummyReturnResult = Task.FromResult(ExpectedReturn);
            }

            protected async override void Because()
            {
                ActualReturn = await (UnderTest as IRequestHandler<DummyCommand, DummyReturn>).Handle(ExpectedCommand, CancellationToken);
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
