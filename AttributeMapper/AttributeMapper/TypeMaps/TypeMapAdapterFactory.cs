using AttributeMapper.TypeMaps.Contracts;
using AttributeMapper.TypeMaps.Maps;

namespace AttributeMapper.TypeMaps
{
    internal class TypeMapAdapterFactory : ITypeMapAdapterFactory
    {
        public ITypeMap<TFrom, TTo> Manufacture<TFrom, TTo>(IFlexibleTypeMap flexibleMap)
        {
            return new TypeMapAdapter<TFrom, TTo>(flexibleMap);
        }
    }
}