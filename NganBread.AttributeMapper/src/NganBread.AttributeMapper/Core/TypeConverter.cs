using System.Reflection;
using NganBread.AttributeMapper.Core.Contracts;

namespace NganBread.AttributeMapper.Core
{
    internal class TypeConverter : ITypeConverter
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
                var converter = _typeMapContainer.ResolveMap<TFrom, TTo>();
                return converter.ConvertCore(source, toType);
            }

            //We might be able to convert implicitly
            if (CanConvertImplicitly<TFrom, TTo>(source))
            {
                return ConvertImplicitly<TFrom, TTo>(source);
            }

            //default fallback
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
            if (source.GetType().GetTypeInfo().IsValueType || toType.GetTypeInfo().IsValueType) return true;

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
                // ReSharper disable once UnusedVariable
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