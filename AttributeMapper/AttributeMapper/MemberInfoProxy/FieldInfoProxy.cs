using System;
using System.Collections.Generic;
using System.Reflection;

namespace AttributeMapper.MemberInfoProxy
{
    public class FieldInfoProxy : MemberInfoProxyBase
    {
        private readonly FieldInfo _fieldInfo;

        public FieldInfoProxy(FieldInfo fieldInfo, IEnumerable<string> aliases)
            :base(fieldInfo, aliases)
        {
            _fieldInfo = fieldInfo;
        }

        public override Type MemberType { get { return _fieldInfo.FieldType; } }


        public override object GetValue(object context)
        {
            return _fieldInfo.GetValue(context);
        }

        public override void SetValue(object context, object value)
        {
            _fieldInfo.SetValue(context, value);
        }
    }
}