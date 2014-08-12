using System;

namespace AttributeMapper.Maps
{
    public interface ITypeMap
    {
        object ConvertCore(object source, Type toType);
        bool CanConvertCore(Type fromType, Type toType);
    }
}