using Com.Melon.Core.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Melon.Core.Unit.Test.Application
{
    public class DummyCommand: CommandBase
    {
    }

    public class DummyCommandWithResult : CommandBase<DummyResult>
    {
    }
}
