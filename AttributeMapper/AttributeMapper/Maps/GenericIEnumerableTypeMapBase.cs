using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AttributeMapper.Maps
{
    public class GenericIEnumerableTypeMapBase : EnumerableTypeMapBase
    {
        public GenericIEnumerableTypeMapBase()
            : base(typeof(IEnumerable<>))
        {
        }

        public override object ConvertCore(object source, Type toType)
        {
            return source == null
                ? null
                : (source as IEnumerable).Cast<object>().Select(x => AttributeMapper.Map(x, toType));
        }
    }
}