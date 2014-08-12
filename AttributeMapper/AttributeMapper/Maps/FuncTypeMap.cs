using System;

namespace AttributeMapper.Maps
{
    public class FuncTypeMap<TFrom, TTo> : TypeMapBase<TFrom, TTo>
    {
        private readonly Func<TFrom, TTo> _mapper;

        public FuncTypeMap(Func<TFrom, TTo> mapper)
        {
            _mapper = mapper;
        }

        public override TTo Convert(TFrom source, Type toType)
        {
            return _mapper(source);
        }
    }
}