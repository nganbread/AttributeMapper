using NganBread.AttributeMapper.TypeMaps.Contracts;
using NganBread.AttributeMapper.TypeMaps.Maps;

namespace NganBread.AttributeMapper.TypeMaps
{
    internal class TypeMapAdapterFactory : ITypeMapAdapterFactory
    {
        public ITypeMap<TFrom, TTo> Manufacture<TFrom, TTo>(IFlexibleTypeMap flexibleMap)
        {
            return new TypeMapAdapter<TFrom, TTo>(flexibleMap);
        }
    }
}