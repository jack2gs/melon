﻿using MediatR;

namespace Com.Melon.Core.Application
{
    public interface ICommandHandler<in TCommand>
        where TCommand: ICommand
    {
        void Execute(TCommand command);
    }

    public interface ICommandHandler<in TCommand, out TResult>
        where TCommand : ICommand<TResult>
    {
        TResult Execute(TCommand command);
    }
}
