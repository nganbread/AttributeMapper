using System;
using AttributeMapper.Maps;

namespace AttributeMapper
{
    public static class Converter
    {
        public static TOut CastOrDefault<TOut>(object o)
        {
            if (o == null) return default(TOut);
            try
            {
                return (TOut)o;
            }
            catch
            {
                return default(TOut);
            }
        }

        public static object SafeConvert(object o, Type toType)
        {
            if (o == null)
            {
                //we dont now the source type, so return the default of the to type
                return toType.IsValueType
                    ? Activator.CreateInstance(toType)
                    : null;
            }

            //try find a map to resolve with
            if (TypeMaps.CanResolveMap(o.GetType(), toType))
            {
                var mapper = TypeMaps.ResolveMap(o.GetType(), toType);
                return mapper.ConvertCore(o, toType);
            }

            //implicitly convert
            var converterType = typeof(InternalConverter<,>).MakeGenericType(o.GetType(), toType);
            var converter = Activator.CreateInstance(converterType, true);
            return converterType.GetMethod("SafeConvert", new []{ o.GetType() }).Invoke(converter, new[]{o});
        }

        public static bool CanConvert(object o, Type toType)
        {
            //we can convert null to default
            if (o == null) return true;

            //value types are either implicitly convertible or should be set to default
            if (o.GetType().IsValueType || toType.IsValueType) return true;

            //see if we have a registered type
            if (TypeMaps.CanResolveMap(o.GetType(), toType)) return true;

            //We might be able to convert implicitly
            var converterType = typeof(InternalConverter<,>).MakeGenericType(o.GetType(), toType);
            var converter = Activator.CreateInstance(converterType, true);
            return (bool)converterType.GetMethod("CanConvert", new[] { o.GetType() }).Invoke(converter, new[] { o });
        }

        private class InternalConverter<TIn, TOut>
        {
            private InternalConverter()
            {

            }

            public bool CanConvert(TIn @in)
            {
                try
                {
                    var x = (TOut)(dynamic)@in;
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public TOut SafeConvert(TIn @in)
            {
                try
                {
                    return (TOut)(dynamic)@in;
                }
                catch
                {
                    return default(TOut);
                }
            }
        }
    }
    
}