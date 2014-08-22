using System;
using System.Collections.Generic;

namespace AttributeMapper.Core.Contracts
{
    public interface IMemberInfoProxy
    {
        Type MemberType { get; }
        IList<string> Names { get; }
        object GetValue(object context);
        void SetValue(object context, object value);
    }
}