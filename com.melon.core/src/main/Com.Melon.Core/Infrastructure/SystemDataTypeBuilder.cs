using System;

namespace Com.Melon.Core.Infrastructure
{
    public class SystemDataTypeBuilder : ISystemDataTypeBuilder
    {
        public ISystemDataType Build(Type type)
        {
            return new SystemDataType(type);
        }
    }
}
