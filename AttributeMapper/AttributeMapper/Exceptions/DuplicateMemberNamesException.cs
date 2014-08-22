using System;
using System.Collections.Generic;
using System.Linq;

namespace AttributeMapper.Exceptions
{
    public class DuplicateMemberNamesException<T> : Exception
    {
        public DuplicateMemberNamesException(IList<string> propertyNames)
            : base(String.Format("Duplicate property names were found on {0}: {1}", typeof(T), String.Join(", ", propertyNames.Where(x => propertyNames.Count(y => y.Equals(x)) > 1))))
        {
        }
    }
}
