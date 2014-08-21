using System.Collections.Generic;
using AttributeMapper.Attributes;

namespace AttributeMapper.Test.Integration.MapTo.Poco
{
    public class Source
    {
        [MapTo("DestinationObject")]
        public NestedSource SourceObject { get; set; }
        [MapTo("DestinationObjects")]
        public IEnumerable<NestedSource> SourceObjects { get; set; }
        [MapTo("DestinationString")]
        public string SourceString { get; set; }
        [MapTo("DestinationInt")]
        public int SourceInt { get; set; }
        [MapTo("DestinationEnum")]
        public SourceEnum SourceEnum { get; set; }
    }
}
