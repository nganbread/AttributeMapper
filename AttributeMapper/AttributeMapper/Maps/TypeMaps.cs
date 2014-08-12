using System;
using System.Collections.Generic;
using System.Linq;

namespace AttributeMapper.Maps
{
    public static class TypeMaps
    {
        private static readonly ISet<ITypeMap> _typeMaps = new HashSet<ITypeMap>
        {
            //new EnumerableTypeMap(),
            new GenericIEnumerableTypeMapBase()
        };

        public static void Register<TFrom, TTo>(Func<TFrom, TTo> map)
        {
            Register(new FuncTypeMap<TFrom, TTo>(map));
        }

        public static void Register(ITypeMap map)
        {
            _typeMaps.Add(map);
        }

        public static ITypeMap ResolveMap(Type from, Type to)
        {
            var map = _typeMaps.SingleOrDefault(x => x.CanConvertCore(from, to));

            if(map == null) throw new Exception("Type resolution failed");

            return map;
        }

        public static bool CanResolveMap(Type from, Type to)
        {
            return _typeMaps.Any(x => x.CanConvertCore(from, to));
        }

        public static bool CanResolveMap<TFrom, TTo>()
        {
            return CanResolveMap(typeof (TFrom), typeof (TTo));
        }
    }
}