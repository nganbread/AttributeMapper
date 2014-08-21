using System;
using System.Collections.Generic;
using System.Linq;

namespace AttributeMapper.Exceptions
{
    public class DuplicatePropertyNamesException<T> : Exception
    {
        public DuplicatePropertyNamesException(IEnumerable<T> objects)
            : base(String.Format("Duplicate property names were found on {0}: {1}", typeof(T).Name, String.Join(", ", objects.Where(x => objects.Count(y => y.Equals(x)) > 1))))
        {
        }
    }
}
