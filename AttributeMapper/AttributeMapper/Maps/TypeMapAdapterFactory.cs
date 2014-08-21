using AttributeMapper.Maps.Contracts;
using AttributeMapper.TypeConverters;

namespace AttributeMapper.Maps
{
    public class TypeMapAdapterFactory : ITypeMapAdapterFactory
    {
        public ITypeMap<TFrom, TTo> Manufacture<TFrom, TTo>(IFlexibleTypeMap flexibleMap)
        {
            return new TypeMapAdapter<TFrom, TTo>(flexibleMap);
        }
    }
}