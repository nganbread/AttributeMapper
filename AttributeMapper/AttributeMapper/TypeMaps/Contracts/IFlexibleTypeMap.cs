using System;

namespace AttributeMapper.TypeMaps.Contracts
{
    public interface IFlexibleTypeMap : ITypeMap<object, object>
    {
        bool CanConvert(Type fromType, Type toType);
    }
}