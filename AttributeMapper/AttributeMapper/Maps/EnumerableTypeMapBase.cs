using System;
using System.Collections;
using System.Linq;

namespace AttributeMapper.Maps
{
    public abstract class EnumerableTypeMapBase<T> : EnumerableTypeMapBase
    {
        protected EnumerableTypeMapBase()
            : base(typeof(T))
        {
        }

        public override object ConvertCore(object source, Type toType)
        {
            if (source == null) return null;
            return Convert(source as IEnumerable);
        }

        public abstract T Convert(IEnumerable enumerable);
    }

    public abstract class EnumerableTypeMapBase : ITypeMap
    {
        private readonly Type _enumerableType;

        protected EnumerableTypeMapBase(Type enumerableType)
        {
            _enumerableType = enumerableType;
        }

        public abstract object ConvertCore(object source, Type toType);

        public bool CanConvertCore(Type fromType, Type toType)
        {
            return
                fromType.GetInterfaces().Any(x => x == typeof(IEnumerable)) != null &&
                (_enumerableType).IsAssignableFrom(toType);
        }
    }
}