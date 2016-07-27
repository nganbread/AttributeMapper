using System.Collections.Generic;
using System.Linq;
using NganBread.AttributeMapper.Core.Contracts;
using NganBread.AttributeMapper.Exceptions;

namespace NganBread.AttributeMapper.Core
{
    internal class MemberInfoVerifier : IMemberInfoVerifier
    {
        public void Verify<T>(IList<IMemberInfoProxy> properties)
        {
            var names = properties.SelectMany(x => x.Names.Distinct()).ToList();
            if (names.Count != names.Distinct().Count())
            {
                throw new DuplicateMemberNamesException<T>(names);
            }   
        }
    }
}