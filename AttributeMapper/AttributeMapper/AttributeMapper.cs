using System;
using System.Linq;
using System.Reflection;
using AttributeMapper.Attributes;
using AttributeMapper.Maps;
using AttributeMapper.TypeConverters;

namespace AttributeMapper
{
    public static class AttributeMapper
    {
        static AttributeMapper()
        {
            TypeConverter = new TypeConverter(new TypeMapContainer());
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

            var sourceProperties = sourceType.GetProperties();
            var destinationProperties = typeof (TTo)
                                            .GetProperties()
                                            .Select(x => new
                                            {
                                                PropertyInfo = x,
                                                Names = GetFromNames(x)
                                            });
            
            //Create an instance of the destination if we can
            if (typeof (TTo).IsAbstract || typeof (TTo).IsInterface) return default(TTo);
            var destination = Activator.CreateInstance<TTo>();

            //Map over public properties
            foreach (var destinationProperty in destinationProperties)
            {
                //check that there is a matching property to map from
                var sourceProperty = sourceProperties.SingleOrDefault(x => destinationProperty.Names.Contains(x.Name));
                if (sourceProperty == null) continue;
                
                var sourcePropertyValue = sourceProperty.GetValue(source);
                var sourcePropertyType = sourceProperty.PropertyType;
                var destinationPropertyType = destinationProperty.PropertyInfo.PropertyType;
                var destinationPropertyValue = MapExplicit(sourcePropertyValue, sourcePropertyType, destinationPropertyType);

                destinationProperty.PropertyInfo.SetValue(destination, destinationPropertyValue);
            }

            return destination;
        }

        private static string[] GetFromNames(PropertyInfo propertyInfo)
        {
            return
                propertyInfo
                    .GetCustomAttributes(typeof (MapFromAttribute), true)
                    .Cast<MapFromAttribute>()
                    .Select(x => x.PropertyName)
                    .Concat(new []{ propertyInfo.Name })
                    .Distinct()
                    .ToArray();
        }
    }
}
