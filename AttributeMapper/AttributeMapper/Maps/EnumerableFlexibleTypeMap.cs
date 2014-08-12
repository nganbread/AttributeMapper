using System;
using System.Collections;
using System.Collections.Generic;
using AttributeMapper.Maps.Contracts;

namespace AttributeMapper.Maps
{
    public class EnumerableFlexibleTypeMap : IFlexibleTypeMap
    {
        public object ConvertCore(object source, Type toType)
        {
            var fromEnumerableType = source.GetType().GetGenericArguments()[0];
            var toEnumerableType = toType.GetGenericArguments()[0];

            var genericListType = typeof (List<>).MakeGenericType(toEnumerableType);
            var genericList = Activator.CreateInstance(genericListType) as IList;

            foreach (var o in source as IEnumerable)
            {
                genericList.Add(AttributeMapper.MapExplicit(o, fromEnumerableType, toEnumerableType));
            }

            return genericList;
        }

        public bool CanConvert(Type fromType, Type toType)
        {
            return
                fromType.IsGenericType &&
                toType.IsGenericType &&
                typeof(IEnumerable<>).IsAssignableFrom(fromType.GetGenericTypeDefinition()) && 
                typeof(IEnumerable<>).IsAssignableFrom(toType.GetGenericTypeDefinition());
        }
    }
}
