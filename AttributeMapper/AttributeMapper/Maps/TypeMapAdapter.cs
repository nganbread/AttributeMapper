using System;
using AttributeMapper.Maps.Contracts;

namespace AttributeMapper.Maps
{
    public class TypeMapAdapter<TFrom, TTo> : ITypeMap<TFrom, TTo>
    {
        private readonly IFlexibleTypeMap _flexibleTypeMap;

        public TypeMapAdapter(IFlexibleTypeMap flexibleTypeMap)
        {
            _flexibleTypeMap = flexibleTypeMap;
        }

        public TTo ConvertCore(TFrom source, Type toType)
        {
            return (TTo)_flexibleTypeMap.ConvertCore(source, toType);
        }
    }
}