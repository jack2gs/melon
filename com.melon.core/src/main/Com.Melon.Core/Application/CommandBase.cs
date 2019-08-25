using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Melon.Core.Application
{
    public abstract class CommandBase: IRequest, ICommand
    {
    }

    public abstract class CommandBase<TResult> : IRequest<TResult>, ICommand<TResult>
    {
    }
}
