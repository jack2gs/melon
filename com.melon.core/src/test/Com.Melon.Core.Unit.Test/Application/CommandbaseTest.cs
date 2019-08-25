using Com.Melon.Core.Application;
using MediatR;
using XunitExtensions;
using FluentAssertions;

namespace Com.Melon.Core.Unit.Test.Application
{
    public class Given_a_commandbase: Specification
    {
        public class When_after_concreted : Given_a_commandbase
        {
            protected DummyCommand Command { get; set; }

            protected override void EstablishContext()
            {
                base.EstablishContext();
                Command = new DummyCommand();
            }

            [Observation]
            void should_be_assignable_ICommand_interface()
            {
                Command.Should().BeAssignableTo<ICommand>();
            }

            [Observation]
            void should_be_assignable_IRequest_interface()
            {
                Command.Should().BeAssignableTo<IRequest>();
            }

            protected class DummyCommand : CommandBase
            {

            }
        }
    }
}
