using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AttributeMapper.Core
{
    public abstract class MemberInfoProxyBase : IMemberInfoProxy
    {
        private readonly IList<string> _names;

        protected MemberInfoProxyBase(MemberInfo fieldInfo, IEnumerable<string> aliases)
        {
            _names = aliases
                .Concat(new[] { fieldInfo.Name })
                .Distinct()
                .ToList();
        }

        public abstract Type MemberType { get; }

        public IList<string> Names
        {
            get { return _names; }
        }

        public abstract object GetValue(object context);
        public abstract void SetValue(object context, object value);
    }
}