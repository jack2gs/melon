using Com.Melon.Core.Application;

namespace Com.Melon.Core.Integration.Test.Application.Dummy
{
    public class DummyCommandHandler : CommandHandlerBase<DummyCommand>
    {
        public bool IsExecuteCalled { get; private set; }

        public DummyCommand ReceievedCommand { get; private set; }

        public override void Execute(DummyCommand command)
        {
            IsExecuteCalled = true;
            ReceievedCommand = command;
        }
    }
}
