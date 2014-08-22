using System;

namespace AttributeMapper.Core.Contracts
{
    public interface IMapper
    {
        object MapExplicit(object source, Type fromType, Type toType);
        TTo Map<TFrom, TTo>(TFrom @from);
    }
}