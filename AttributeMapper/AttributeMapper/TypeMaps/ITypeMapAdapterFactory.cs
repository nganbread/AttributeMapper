using AttributeMapper.TypeMaps.Contracts;

namespace AttributeMapper.TypeMaps
{
    public interface ITypeMapAdapterFactory
    {
        ITypeMap<TFrom,TTo> Manufacture<TFrom, TTo>(IFlexibleTypeMap flexibleMap);
    }
}