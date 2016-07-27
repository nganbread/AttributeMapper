using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NganBread.AttributeMapper.Core.Contracts;
using NganBread.AttributeMapper.TypeMaps.Contracts;

namespace NganBread.AttributeMapper.TypeMaps.Maps
{
    internal class EnumerableFlexibleTypeMap : IFlexibleTypeMap
    {
        private readonly IMapper _mapper;

        public EnumerableFlexibleTypeMap(IMapper mapper)
        {
            _mapper = mapper;
        }

        public object ConvertCore(object source, Type toType)
        {
            var fromEnumerableType = source.GetType().GetTypeInfo().GetGenericArguments()[0];
            var toEnumerableType = toType.GetTypeInfo().GetGenericArguments()[0];

            var genericListType = typeof (List<>).MakeGenericType(toEnumerableType);
            var genericList = Activator.CreateInstance(genericListType) as IList;

            foreach (var o in source as IEnumerable)
            {
                genericList.Add(_mapper.MapExplicit(o, fromEnumerableType, toEnumerableType));
            }

            return genericList;
        }

        public bool CanConvert(Type fromType, Type toType)
        {
            return
                fromType.GetTypeInfo().IsGenericType &&
                toType.GetTypeInfo().IsGenericType &&
                typeof(IEnumerable<>).GetTypeInfo().IsAssignableFrom(fromType.GetGenericTypeDefinition()) && 
                typeof(IEnumerable<>).GetTypeInfo().IsAssignableFrom(toType.GetGenericTypeDefinition());
        }
    }
}
