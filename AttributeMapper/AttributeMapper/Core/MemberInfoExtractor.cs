using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AttributeMapper.Attributes;
using AttributeMapper.Core.Contracts;

namespace AttributeMapper.Core
{
    public class MemberInfoExtractor : IMemberInfoExtractor
    {
        private readonly IMemberInfoVerifier _memberInfoVerifier;

        public MemberInfoExtractor(IMemberInfoVerifier memberInfoVerifier)
        {
            _memberInfoVerifier = memberInfoVerifier;
        }

        public IList<IMemberInfoProxy> ExtractFromMembers<TContext, TFrom>()
        {
            var fields = typeof(TContext)
                .GetFields()
                .Where(x => !x.IsInitOnly)
                .Select<FieldInfo, IMemberInfoProxy>(x => new FieldInfoProxy(x, GetAliases<TFrom, MapFromAttribute>(x)));

            var properties = typeof(TContext)
                .GetProperties()
                .Where(x => x.CanWrite)
                .Select<PropertyInfo, IMemberInfoProxy>(x => new PropertyInfoProxy(x, GetAliases<TFrom, MapFromAttribute>(x)));

            var members = fields.Concat(properties).ToList();
            _memberInfoVerifier.Verify<TContext>(members);
            return members;
        }

        public IList<IMemberInfoProxy> ExtractToMembers<TContext, TTo>()
        {
            var fields = typeof(TContext)
                .GetFields()
                .Where(x => !x.IsInitOnly)
                .Select<FieldInfo, IMemberInfoProxy>(x => new FieldInfoProxy(x, GetAliases<TTo, MapToAttribute>(x)));

            var properties = typeof(TContext)
                .GetProperties()
                .Where(x => x.CanWrite)
                .Select<PropertyInfo, IMemberInfoProxy>(x => new PropertyInfoProxy(x, GetAliases<TTo, MapToAttribute>(x)));

            var members = fields.Concat(properties).ToList();
            _memberInfoVerifier.Verify<TContext>(members);
            return members;
        }
        
        private static IEnumerable<string> GetAliases<T, TAttribute>(MemberInfo propertyInfo)
            where TAttribute : MapAttributeBase
        {
            return
                propertyInfo
                    .GetCustomAttributes(typeof (TAttribute), true)
                    .Cast<TAttribute>()
                    .Where(x => x.Type == null || x.Type == typeof (T))
                    .Select(x => x.PropertyName);
        }
    }
}