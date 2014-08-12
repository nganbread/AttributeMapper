using System;

namespace AttributeMapper.Maps
{
    public abstract class TypeMapBase<TFrom, TTo> : ITypeMap
    {
        public abstract TTo Convert(TFrom source, Type toType);
        public object ConvertCore(object source, Type toType)
        {
            return Convert((TFrom)source, toType);
        }

        public virtual bool CanConvert<TFrom2, TTo2>()
        {
            return CanConvertCore(typeof (TFrom2), typeof (TTo2));
        }

        public bool CanConvertCore(Type fromType, Type toType)
        {
            return fromType == typeof(TFrom) && toType == typeof(TTo);
        }
    }
}