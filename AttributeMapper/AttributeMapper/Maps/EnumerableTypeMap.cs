using System.Collections;

namespace AttributeMapper.Maps
{
    public class EnumerableTypeMap : EnumerableTypeMapBase<IEnumerable>
    {
        public override IEnumerable Convert(IEnumerable enumerable)
        {
            //its just an IList, the implementation doesnt matter so neither does the type of the contents
            return enumerable;
        }
    }
}