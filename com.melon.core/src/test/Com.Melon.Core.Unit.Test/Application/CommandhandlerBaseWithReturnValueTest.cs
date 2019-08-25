using Com.Melon.Core.Application;
using FluentAssertions;
using MediatR;
using System.Threading;
using XunitExtensions;

namespace Com.Melon.Core.Unit.Test.Application
{
    public class Given_a_commandhandlerbase_with_return_value : Specification
    {
        protected DummyImplementationCommandHandler UnderTest;

        protected DummyCommand ExpectedCommand;

        protected DummyResult ExpectedResult;

        protected DummyResult ActuralResult;

        protected class DummyCommand : CommandBase<DummyResult>
        {

        }

        protected class DummyResult
        {

        }

        protected class DummyImplementationCommandHandler : CommandHandlerBase<DummyCommand, DummyResult>
        {
            public DummyCommand ReceievedCommand { get; private set; }

            public DummyResult ReturnResult { get; set; }

            public bool IsExecuteCalled { get; private set; }

            public override DummyResult Execute(DummyCommand command)
            {
                ReceievedCommand = command;
                IsExecuteCalled = true;

                return ReturnResult;
            }
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            UnderTest = new DummyImplementationCommandHandler();
        }

        public class When_after_conreted : Given_a_commandhandlerbase_with_return_value
        {
            [Observation]
            void should_be_assignable_to_ICommandHandler()
            {
                UnderTest.Should().BeAssignableTo<ICommandHandler<DummyCommand, DummyResult>>();
            }

            [Observation]
            void should_be_assignable_to_IRequestHandler()
            {
                UnderTest.Should().BeAssignableTo<IRequestHandler<DummyCommand, DummyResult>>();
            }
        }

        public class When_execute : Given_a_commandhandlerbase_with_return_value
        {
            protected override void EstablishContext()
            {
                base.EstablishContext();
                UnderTest = new DummyImplementationCommandHandler();
                ExpectedCommand = new DummyCommand();
                ExpectedResult = new DummyResult();
                UnderTest.ReturnResult = ExpectedResult;
            }

            protected override void Because()
            {
                ActuralResult = UnderTest.Execute(ExpectedCommand);
            }

            [Observation]
            void should_receieve_the_command()
            {
                UnderTest.ReceievedCommand.Should().Be(ExpectedCommand);
            }

            [Observation]
            void should_recevieve_the_result()
            {
                ActuralResult.Should().Be(ExpectedResult);
            }
        }

        public class When_execute_handler : Given_a_commandhandlerbase_with_return_value
        {
            protected override void Because()
            {
                (UnderTest as IRequestHandler<DummyCommand, DummyResult>).Handle(ExpectedCommand, new CancellationTokenSource().Token);
            }

            [Observation]
            void should_receieve_the_command()
            {
                UnderTest.ReceievedCommand.Should().Be(ExpectedCommand);
            }

            [Observation]
            void should_call_execute()
            {
                UnderTest.IsExecuteCalled.Should().BeTrue();
            }
        }
    }
}
