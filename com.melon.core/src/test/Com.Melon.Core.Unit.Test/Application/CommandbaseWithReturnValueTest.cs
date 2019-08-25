using Com.Melon.Core.Application;
using FluentAssertions;
using MediatR;
using XunitExtensions;

namespace Com.Melon.Core.Unit.Test.Application
{
    public class Given_a_commandbase_with_return_value: Specification
    {
        public class When_after_concreted : Given_a_commandbase_with_return_value
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
                Command.Should().BeAssignableTo<ICommand<DummyReturn>>();
            }

            [Observation]
            void should_be_assignable_IRequest_interface()
            {
                Command.Should().BeAssignableTo<IRequest<DummyReturn>>();
            }

            protected class DummyReturn
            {

            }

            protected class DummyCommand: CommandBase<DummyReturn>
            {

            }
        }
    }
}
