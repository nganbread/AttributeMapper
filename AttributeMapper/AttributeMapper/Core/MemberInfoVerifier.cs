using System.Collections.Generic;
using System.Linq;
using AttributeMapper.Core.Contracts;
using AttributeMapper.Exceptions;

namespace AttributeMapper.Core
{
    internal class MemberInfoVerifier : IMemberInfoVerifier
    {
        public void Verify<T>(IList<IMemberInfoProxy> properties)
        {
            var names = properties.SelectMany(x => x.Names.Distinct()).ToList();
            if (names.Count() != names.Distinct().Count())
            {
                throw new DuplicateMemberNamesException<T>(names);
            }   
        }
    }
}