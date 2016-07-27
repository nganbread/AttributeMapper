using NganBread.AttributeMapper.TypeMaps.Contracts;

namespace NganBread.AttributeMapper.TypeMaps
{
    public interface ITypeMapAdapterFactory
    {
        ITypeMap<TFrom,TTo> Manufacture<TFrom, TTo>(IFlexibleTypeMap flexibleMap);
    }
}