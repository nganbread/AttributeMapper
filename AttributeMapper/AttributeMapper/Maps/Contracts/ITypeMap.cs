using System;

namespace AttributeMapper.Maps.Contracts
{
    public interface ITypeMap<in TFrom, out TTo> : ITypeMap
    {
        TTo ConvertCore(TFrom source, Type toType);
    }

    public interface ITypeMap
    {
        object ConvertCore(object source, Type toType);
    }
}