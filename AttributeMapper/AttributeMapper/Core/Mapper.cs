using System;
using System.Collections.Generic;
using System.Linq;
using AttributeMapper.Core.Contracts;

namespace AttributeMapper.Core
{
    public class Mapper : IMapper
    {
        private readonly ITypeConverter _typeConverter;
        private readonly IMemberInfoExtractor _memberInfoExtractor;

        public Mapper(ITypeConverter typeConverter, IMemberInfoExtractor memberInfoExtractor)
        {
            _typeConverter = typeConverter;
            _memberInfoExtractor = memberInfoExtractor;
        }

        public object MapExplicit(object source, Type fromType, Type toType)
        {
            var method = GetType().GetMethod("Map").MakeGenericMethod(fromType, toType);
            return method.Invoke(this, new[] { source });
        }

        public TTo Map<TFrom, TTo>(TFrom @from)
        {
            //If there is an implicit conversion or registered mapper then do it
            if (_typeConverter.CanConvert<TFrom, TTo>(@from))
            {
                return _typeConverter.SafeConvert<TFrom, TTo>(@from);
            }

            //We cant create an abstract type or interface
            if (typeof(TTo).IsAbstract || typeof(TTo).IsInterface) return default(TTo);

            //Create an instance of the destination
            var destination = Activator.CreateInstance<TTo>();

            //Map over the individual fields/properties
            var fromMembers = _memberInfoExtractor.ExtractToMembers<TFrom, TTo>();
            var toMembers = _memberInfoExtractor.ExtractFromMembers<TTo, TFrom>();
            
            //Map over public field/properties
            foreach (var toMember in toMembers)
            {
                //check that there is a matching field/property to map from
                var fromMember = FindMatchingMember(toMember, fromMembers);
                if (fromMember == null) continue;

                //perform the mapping
                var fromMemberType = fromMember.MemberType;
                var fromMemberValue = fromMember.GetValue(@from);
                var toMemberType = toMember.MemberType;
                var toMemberValue = MapExplicit(fromMemberValue, fromMemberType, toMemberType);

                toMember.SetValue(destination, toMemberValue);
            }

            return destination;
        }

        private static IMemberInfoProxy FindMatchingMember(IMemberInfoProxy toMember, IEnumerable<IMemberInfoProxy> fromMembers)
        {
            foreach (var fromMember in fromMembers)
            {
                if (fromMember.Names.Any(fromName => toMember.Names.Contains(fromName)))
                {
                    return fromMember;
                }
            }

            return null;
        }
    }
}