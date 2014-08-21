using AttributeMapper.Maps.Contracts;

namespace AttributeMapper.Maps
{
    public interface ITypeMapAdapterFactory
    {
        ITypeMap<TFrom,TTo> Manufacture<TFrom, TTo>(IFlexibleTypeMap flexibleMap);
    }
}