using System;
using System.Collections.Generic;
using System.Reflection;

namespace AttributeMapper.MemberInfoProxy
{
    public class PropertyInfoProxy : MemberInfoProxyBase
    {
        private readonly PropertyInfo _propertyInfo;

        public PropertyInfoProxy(PropertyInfo propertyInfo, IEnumerable<string> aliases)
            : base(propertyInfo, aliases)
        {
            _propertyInfo = propertyInfo;
        }

        public override Type MemberType { get { return _propertyInfo.PropertyType; } }
        
        public override object GetValue(object context)
        {
            return _propertyInfo.GetValue(context);
        }

        public override void SetValue(object context, object value)
        {
            _propertyInfo.SetValue(context, value);
        }
    }
}