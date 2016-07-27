using System.Collections.Generic;
using NganBread.AttributeMapper.Attributes;

namespace NganBread.AttributeMapper.Test.Net461.Integration.MapFrom.Poco
{
    public class Destination
    {
        [MapFrom(nameof(Source.SourceObject))]
        public NestedDestination DestinationObject { get; set; }

        [MapFrom(nameof(Source.SourceObjects))]
        public IEnumerable<NestedDestination> DestinationObjects { get; set; }

        [MapFrom(nameof(Source.SourceString))]
        public string DestinationString { get; set; }

        [MapFrom(nameof(Source.SourceInt))]
        public int DestinationInt { get; set; }

        [MapFrom(nameof(Source.SourceEnum))]
        public DestinationEnum DestinationEnum { get; set; }
        
        [MapFrom(nameof(Source.SourceField))]
        public int DestinationField;
    }
}