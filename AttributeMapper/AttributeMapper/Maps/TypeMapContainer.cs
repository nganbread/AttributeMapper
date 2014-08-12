using System;
using System.Collections.Generic;
using System.Linq;
using AttributeMapper.Maps.Contracts;

namespace AttributeMapper.Maps
{
    public class TypeMapContainer : ITypeMapContainer
    {
        private readonly ISet<Type> _typeMaps = new HashSet<Type>();
        private readonly IList<IFlexibleTypeMap> _flexibleTypeMaps = new List<IFlexibleTypeMap>
        {
            new EnumerableFlexibleTypeMap()
        };

        public void RegisterMap<TFlexibleTypeMap>(TFlexibleTypeMap map)
            where TFlexibleTypeMap : IFlexibleTypeMap
        {
            _flexibleTypeMaps.Add(map);
        }

        public void RegisterMap<TTypeMap, TFrom, TTo>()
            where TTypeMap: ITypeMap<TFrom, TTo>
        {
            if(typeof(TTypeMap).IsInterface || typeof(TTypeMap).IsAbstract) throw new Exception("Must register a concrete type");

            _typeMaps.Add(typeof(TTypeMap));
        }

        public bool CanResolveMap<TFrom, TTo>()
        {
            return _typeMaps.Any(x => x == typeof (ITypeMap<TFrom, TTo>)) ||
                   _flexibleTypeMaps.Any(x => x.CanConvert(typeof (TFrom), typeof (TTo)));
        }

        public ITypeMap ResolveMap<TFrom, TTo>()
        {
            var mapType = _typeMaps.SingleOrDefault(x => x == typeof(ITypeMap<TFrom, TTo>));
            if (mapType != null)
            {
                var map = Activator.CreateInstance(mapType);
                return map as ITypeMap;
            }

            var flexibleMap = _flexibleTypeMaps.LastOrDefault(x => x.CanConvert(typeof (TFrom), typeof (TTo)));
            if (flexibleMap != null)
            {
                return flexibleMap;
            }

            throw new Exception("Type resolution failed");
        }
    }
}