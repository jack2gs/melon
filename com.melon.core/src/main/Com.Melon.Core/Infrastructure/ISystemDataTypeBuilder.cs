using System;

namespace Com.Melon.Core.Infrastructure
{
    public interface ISystemDataTypeBuilder
    {
        ISystemDataType Build(Type type);
    }
}
