using System.Collections.Generic;
using NganBread.AttributeMapper.Attributes;

namespace NganBread.AttributeMapper.Test.Net461.Integration.MapTo.Poco
{
    public class Source
    {
        [MapTo(nameof(Destination.DestinationObject))]
        public NestedSource SourceObject { get; set; }

        [MapTo(nameof(Destination.DestinationObjects))]
        public IEnumerable<NestedSource> SourceObjects { get; set; }

        [MapTo(nameof(Destination.DestinationString))]
        public string SourceString { get; set; }

        [MapTo(nameof(Destination.DestinationInt))]
        public int SourceInt { get; set; }

        [MapTo(nameof(Destination.DestinationEnum))]
        public SourceEnum SourceEnum { get; set; }
    }
}
