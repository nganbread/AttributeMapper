using System;
using AttributeMapper.Maps.Contracts;

namespace AttributeMapper.TypeConverters
{
    public class TypeConverter : ITypeConverter
    {
        private readonly ITypeMapContainer _typeMapContainer;

        public TypeConverter(ITypeMapContainer typeMapContainer)
        {
            _typeMapContainer = typeMapContainer;
        }

        public TTo SafeConvert<TFrom, TTo>(TFrom source)
        {
            var toType = typeof (TTo);

            //we can convert null to null or default
            if (source == null)
            {
                return default(TTo);
            }

            //try find a map to resolve with
            if (_typeMapContainer.CanResolveMap<TFrom, TTo>())
            {
                //hmm dangerous?
                var converter = _typeMapContainer.ResolveMap<TFrom, TTo>();
                var converted = converter.ConvertCore(source, toType);
                if (converted is TTo) return (TTo) converted;

                var message = "The type converter ({0}) did not convert the supplied object to a compatible type. Expected: {1}. Received {2}";
                message = String.Format(message, converter, typeof (TTo), converted == null ? null : converted.GetType());
                throw new Exception(message);
            }

            //We might be able to convert implicitly
            if (CanConvertImplicitly<TFrom, TTo>(source))
            {
                return ConvertImplicitly<TFrom, TTo>(source);
            }

            return default(TTo);
        }

        public bool CanConvert<TFrom, TTo>(TFrom source)
        {
            var toType = typeof(TTo);

            //we can convert null to null or default
            if (source == null) return true;

            //see if we have a registered type map
            if (_typeMapContainer.CanResolveMap<TFrom, TTo>()) return true;

            //value types are either implicitly convertible or should be set to default
            if (source.GetType().IsValueType || toType.IsValueType) return true;

            //We might be able to convert implicitly
            if (CanConvertImplicitly<TFrom, TTo>(source)) return true;

            return false;
        }

        private static TTo ConvertImplicitly<TFrom, TTo>(TFrom @in)
        {
            try
            {
                return (TTo)(dynamic)@in;
            }
            catch
            {
                return default(TTo);
            }
        }

        private static bool CanConvertImplicitly<TFrom, TTo>(TFrom @in)
        {
            try
            {
                var temp = (TTo)(dynamic)@in;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}