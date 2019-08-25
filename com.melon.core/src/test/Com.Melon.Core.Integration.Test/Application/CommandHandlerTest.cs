using Com.Melon.Core.Application;
using Com.Melon.Core.Integration.Test.Application.Dummy;
using Com.Melon.Core.Integration.Test.Application.Setup;
using FluentAssertions;
using MediatR;
using Xunit;
using XunitExtensions;

namespace Com.Melon.Core.Integration.Test.Application
{
    public class Given_a_command_handler : Specification, IClassFixture<ApplicationFixture>
    {
        protected DummyCommandHandler DummyCommandHandler;

        protected DummyCommand ExpectedCommand;

        protected IMediator Mediator;

        protected ICommandBus CommandBus;

        protected ApplicationFixture ApplicationFixture;

        public Given_a_command_handler()
        {
            ApplicationFixture = new ApplicationFixture();
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            DummyCommandHandler = ApplicationFixture.BuildDummyCommandHandler();
            Mediator = ApplicationFixture.BuildMediator();
            CommandBus = ApplicationFixture.BuildCommandBus();
            ExpectedCommand = new DummyCommand();
        }

        public class When_command_is_send_by_mediatR : Given_a_command_handler
        {
            protected async override void Because()
            {
                await Mediator.Send(ExpectedCommand);
            }

            [Observation]
            void should_call_execute()
            {
                DummyCommandHandler.IsExecuteCalled.Should().BeTrue();
            }

            [Observation]
            void should_receieve_the_command()
            {
                DummyCommandHandler.ReceievedCommand.Should().Be(ExpectedCommand);
            }
        }

        public class When_command_is_send_by_command_bus : Given_a_command_handler
        {
            protected override void Because()
            {
                CommandBus.Send(ExpectedCommand);
            }

            [Observation]
            void should_call_execute()
            {
                DummyCommandHandler.IsExecuteCalled.Should().BeTrue();
            }

            [Observation]
            void should_receieve_the_command()
            {
                DummyCommandHandler.ReceievedCommand.Should().Be(ExpectedCommand);
            }
        }

        public class When_command_is_send_by_command_bus_asynchorizedly : Given_a_command_handler
        {
            protected async override void Because()
            {
                await CommandBus.AsyncSend(ExpectedCommand);
            }

            [Observation]
            void should_call_execute()
            {
                DummyCommandHandler.IsExecuteCalled.Should().BeTrue();
            }

            [Observation]
            void should_receieve_the_command()
            {
                DummyCommandHandler.ReceievedCommand.Should().Be(ExpectedCommand);
            }
        }
    }
}
