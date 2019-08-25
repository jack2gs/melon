using Com.Melon.Core.Application;

namespace Com.Melon.Core.Integration.Test.Application.Dummy
{
    public class DummyCommandWithResultHandler : CommandHandlerBase<DummyCommandWithResult, DummyResult>
    {
        public bool IsExecuteCalled { get; private set; }

        public DummyCommandWithResult ReceievedCommand { get; private set; }

        public override DummyResult Execute(DummyCommandWithResult command)
        {
            IsExecuteCalled = true;
            ReceievedCommand = command;

            return new DummyResult();
        }
    }
}
