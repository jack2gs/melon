using Com.Melon.Core.Application;
using FluentAssertions;
using MediatR;
using System.Threading;
using XunitExtensions;

namespace Com.Melon.Core.Unit.Test.Application
{
    public class Given_a_commandhandlerbase_and_a_dummy_command : Specification
    {
        protected DummyImplementationCommandHandler UnderTest;

        protected DummyCommand ExpectedCommand;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            UnderTest = new DummyImplementationCommandHandler();
            ExpectedCommand = new DummyCommand();
        }
       
        protected class DummyImplementationCommandHandler : CommandHandlerBase<DummyCommand>
        {
            public DummyCommand ReceievedCommand { get; private set; }

            public bool IsExecuteCalled { get; private set; }

            public override void Execute(DummyCommand command)
            {
                ReceievedCommand = command;
                IsExecuteCalled = true;
            }
        }

        protected class DummyCommand : CommandBase
        {

        }

        public class When_after_concreted : Given_a_commandhandlerbase_and_a_dummy_command
        {
            [Observation]
            void should_be_assignable_to_ICommandHandler()
            {
                UnderTest.Should().BeAssignableTo<ICommandHandler<DummyCommand>>();
            }

            [Observation]
            void should_be_assignable_to_IRequestHandler()
            {
                UnderTest.Should().BeAssignableTo<IRequestHandler<DummyCommand>>();
            }
        }

        public class When_execute : Given_a_commandhandlerbase_and_a_dummy_command
        {
            protected override void Because()
            {
                UnderTest.Execute(ExpectedCommand);
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

        public class When_execute_handler : Given_a_commandhandlerbase_and_a_dummy_command
        {
            protected override void Because()
            {
                (UnderTest as IRequestHandler<DummyCommand>).Handle(ExpectedCommand, new CancellationTokenSource().Token);
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
