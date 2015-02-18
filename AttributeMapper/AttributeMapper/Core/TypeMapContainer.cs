using System;
using System.Collections.Generic;
using System.Linq;
using AttributeMapper.Core.Contracts;
using AttributeMapper.Exceptions;
using AttributeMapper.TypeMaps;
using AttributeMapper.TypeMaps.Contracts;

namespace AttributeMapper.Core
{
    internal class TypeMapContainer : ITypeMapContainer
    {
        private readonly ISet<Type> _typeMaps = new HashSet<Type>();
        private readonly IList<IFlexibleTypeMap> _flexibleTypeMaps = new List<IFlexibleTypeMap>();

        private readonly ITypeMapAdapterFactory _typeMapAdapterFactory;

        public TypeMapContainer(ITypeMapAdapterFactory typeMapAdapterFactory)
        {
            _typeMapAdapterFactory = typeMapAdapterFactory;
        }

        public void RegisterMap<TFlexibleTypeMap>(TFlexibleTypeMap map)
            where TFlexibleTypeMap : IFlexibleTypeMap
        {
            _flexibleTypeMaps.Add(map);
        }

        public void RegisterMap<TTypeMap, TFrom, TTo>()
            where TTypeMap: ITypeMap<TFrom, TTo>
        {
            if(typeof(TTypeMap).IsInterface) throw new CanNotRegisterAnInterfaceTypeException<TTypeMap>();
            if(typeof(TTypeMap).IsAbstract) throw new CanNotRegisterAnAbstractTypeException<TTypeMap>();

            _typeMaps.Add(typeof(TTypeMap));
        }

        public bool CanResolveMap<TFrom, TTo>()
        {
            return _typeMaps.Any(x => x.GetInterfaces().Any(y => y == typeof (ITypeMap<TFrom, TTo>))) ||
                   _flexibleTypeMaps.Any(x => x.CanConvert(typeof (TFrom), typeof (TTo)));
        }

        public ITypeMap<TFrom, TTo> ResolveMap<TFrom, TTo>()
        {
            var mapType = _typeMaps.FirstOrDefault(x => x.GetInterfaces().Any(y => y == typeof(ITypeMap<TFrom, TTo>)));
            if (mapType != null)
            {
                var map = Activator.CreateInstance(mapType);
                return map as ITypeMap<TFrom, TTo>;
            }

            var flexibleMap = _flexibleTypeMaps.LastOrDefault(x => x.CanConvert(typeof (TFrom), typeof (TTo)));
            if (flexibleMap != null)
            {
                return _typeMapAdapterFactory.Manufacture<TFrom, TTo>(flexibleMap);
            }

            throw new Exception("Type map resolution failed");
        }
    }
}