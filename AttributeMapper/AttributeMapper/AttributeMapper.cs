using System;
using System.Linq;
using System.Reflection;
using AttributeMapper.Attributes;

namespace AttributeMapper
{
    public static class AttributeMapper
    {
        public static TOut Map<TOut>(object source)
        {
            //Remove generics
            var mapped = Map(source, typeof (TOut));
            return Converter.CastOrDefault<TOut>(mapped);
        }

        public static object Map(object source, Type destinationType)
        {
            //If there is an implicit conversion or registered mapper then do it
            if (Converter.CanConvert(source, destinationType))
            {
                return Converter.SafeConvert(source, destinationType);
            }

            //Lets create an instance of the destination and map over the individual properties

            var sourceType = source.GetType();

            var sourceProperties = sourceType.GetProperties();
            var destinationProperties = destinationType
                                            .GetProperties()
                                            .Select(x => new
                                            {
                                                PropertyInfo = x,
                                                Names = GetFromNames(x)
                                            });
            
            //Create an instance of the destination
            var destination = Activator.CreateInstance(destinationType);

            //Map over public properties
            bool propertySuccessfullyMapped = false;
            foreach (var destinationProperty in destinationProperties)
            {
                //check that there is a matching property to map from
                var sourceProperty = sourceProperties.SingleOrDefault(x => destinationProperty.Names.Contains(x.Name));
                if (sourceProperty == null) continue;
                
                var sourcePropertyValue = sourceProperty.GetValue(source);
                var destinationPropertyType = destinationProperty.PropertyInfo.PropertyType;
                var destinationPropertyValue =  Map(sourcePropertyValue, destinationPropertyType);

                if (destinationPropertyValue != null)
                {
                    propertySuccessfullyMapped = true;
                    destinationProperty.PropertyInfo.SetValue(destination, destinationPropertyValue);
                }
            }

            //if nothing was mapped then just return null
            return propertySuccessfullyMapped
                ? destination
                : null;
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
