using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NganBread.AttributeMapper.Attributes;
using NganBread.AttributeMapper.Core.Contracts;
using NganBread.AttributeMapper.MemberInfoProxy;

namespace NganBread.AttributeMapper.Core
{
    internal class MemberInfoExtractor : IMemberInfoExtractor
    {
        private readonly IMemberInfoVerifier _memberInfoVerifier;

        public MemberInfoExtractor(IMemberInfoVerifier memberInfoVerifier)
        {
            _memberInfoVerifier = memberInfoVerifier;
        }

        public IList<IMemberInfoProxy> ExtractFromMembers<TContext, TFrom>()
        {
            var fields = typeof(TContext)
                .GetTypeInfo()
                .GetFields()
                .Where(x => !x.IsInitOnly)
                .Select<FieldInfo, IMemberInfoProxy>(x => new FieldInfoProxy(x, GetAliases<TFrom, MapFromAttribute>(x)));

            var properties = typeof(TContext)
                .GetTypeInfo()
                .GetProperties()
                .Where(x => x.GetSetMethod() != null)
                .Select<PropertyInfo, IMemberInfoProxy>(x => new PropertyInfoProxy(x, GetAliases<TFrom, MapFromAttribute>(x)));

            var members = fields.Concat(properties).ToList();
            _memberInfoVerifier.Verify<TContext>(members);
            return members;
        }

        public IList<IMemberInfoProxy> ExtractToMembers<TContext, TTo>()
        {
            var fields = typeof(TContext)
                .GetTypeInfo()
                .GetFields()
                .Where(x => !x.IsInitOnly)
                .Select<FieldInfo, IMemberInfoProxy>(x => new FieldInfoProxy(x, GetAliases<TTo, MapToAttribute>(x)));

            var properties = typeof(TContext)
                .GetTypeInfo()
                .GetProperties()
                .Where(x => x.CanWrite)
                .Select<PropertyInfo, IMemberInfoProxy>(x => new PropertyInfoProxy(x, GetAliases<TTo, MapToAttribute>(x)));

            var members = fields.Concat(properties).ToList();
            _memberInfoVerifier.Verify<TContext>(members);
            return members;
        }
        
        private static IEnumerable<string> GetAliases<T, TAttribute>(MemberInfo memberInfo)
            where TAttribute : MapAttributeBase
        {
            return
                memberInfo
                    .GetCustomAttributes(typeof (TAttribute), true)
                    .Cast<TAttribute>()
                    .Where(x => x.Type == null || x.Type == typeof (T))
                    .Select(x => x.PropertyName);
        }
    }
}