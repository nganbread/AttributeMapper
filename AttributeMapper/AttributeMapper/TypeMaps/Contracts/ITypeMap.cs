using System;

namespace AttributeMapper.TypeMaps.Contracts
{
    public interface ITypeMap<in TFrom, out TTo>
    {
        TTo ConvertCore(TFrom source, Type toType);
    }
}