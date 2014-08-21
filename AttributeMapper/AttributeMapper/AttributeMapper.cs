using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AttributeMapper.Attributes;
using AttributeMapper.Exceptions;
using AttributeMapper.Maps;
using AttributeMapper.TypeConverters;

namespace AttributeMapper
{
    public static class AttributeMapper
    {
        static AttributeMapper()
        {
            TypeConverter = new TypeConverter(new TypeMapContainer(new TypeMapAdapterFactory()));
        }

        private static readonly ITypeConverter TypeConverter;

        public static object MapExplicit(object source, Type fromType, Type toType)
        {
            var method = typeof(AttributeMapper).GetMethod("Map");
            method = method.MakeGenericMethod(fromType, toType);
            return method.Invoke(null, new[] { source });
        }

        public static TTo Map<TFrom, TTo>(TFrom source)
        {
            //If there is an implicit conversion or registered mapper then do it
            if (TypeConverter.CanConvert<TFrom, TTo>(source))
            {
                return TypeConverter.SafeConvert<TFrom, TTo>(source);
            }

            //Lets create an instance of the destination and map over the individual properties
            var sourceType = source.GetType();

            var sourceProperties = sourceType
                                            .GetProperties()
                                            .Select(x => new
                                            {
                                                PropertyInfo = x,
                                                Names = GetToNames<TTo>(x)
                                            });
            var destinationProperties = typeof (TTo)
                                            .GetProperties()
                                            .Select(x => new
                                            {
                                                PropertyInfo = x,
                                                Names = GetFromNames<TFrom>(x)
                                            });
            
            //Create an instance of the destination if we can

            //We cant automatically create an abstract type or interface
            if (typeof (TTo).IsAbstract || typeof (TTo).IsInterface) return default(TTo);
            var destination = Activator.CreateInstance<TTo>();

            //check for duplicates
            VerifyIsDistinct(sourceProperties.SelectMany(x => x.Names));
            VerifyIsDistinct(destinationProperties.SelectMany(x => x.Names));

            //Map over public properties
            foreach (var destinationProperty in destinationProperties)
            {
                //check that there is a matching property to map from
                var sourceProperty = sourceProperties.SingleOrDefault(sp => sp.Names.Any(sourceName => destinationProperty.Names.Contains(sourceName)));
                if (sourceProperty == null) continue;

                var sourcePropertyType = sourceProperty.PropertyInfo.PropertyType;
                var sourcePropertyValue = sourceProperty.PropertyInfo.GetValue(source);
                var destinationPropertyType = destinationProperty.PropertyInfo.PropertyType;
                var destinationPropertyValue = MapExplicit(sourcePropertyValue, sourcePropertyType, destinationPropertyType);

                destinationProperty.PropertyInfo.SetValue(destination, destinationPropertyValue);
            }

            return destination;
        }

        private static void VerifyIsDistinct<T>(IEnumerable<T> names)
        {
            if(names.Count() != names.Distinct().Count()) throw new DuplicatePropertyNamesException<T>(names);
        }

        private static string[] GetFromNames<TFrom>(PropertyInfo propertyInfo)
        {
            return
                propertyInfo
                    .GetCustomAttributes(typeof (MapFromAttribute), true)
                    .Cast<MapFromAttribute>()
                    .Where(x => x.MapFromType == null || x.MapFromType == typeof(TFrom))
                    .Select(x => x.PropertyName)
                    .Concat(new []{ propertyInfo.Name })
                    .Distinct()
                    .ToArray();
        }
        private static string[] GetToNames<TTo>(PropertyInfo propertyInfo)
        {
            return
                propertyInfo
                    .GetCustomAttributes(typeof(MapToAttribute), true)
                    .Cast<MapToAttribute>()
                    .Where(x => x.MapToType == null || x.MapToType == typeof(TTo))
                    .Select(x => x.PropertyName)
                    .Concat(new []{ propertyInfo.Name })
                    .Distinct()
                    .ToArray();
        }
    }
}
