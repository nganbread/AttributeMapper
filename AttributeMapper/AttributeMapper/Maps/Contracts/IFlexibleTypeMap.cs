using System;

namespace AttributeMapper.Maps.Contracts
{
    public interface IFlexibleTypeMap : ITypeMap<object, object>
    {
        bool CanConvert(Type fromType, Type toType);
    }
}