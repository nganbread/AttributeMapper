using System;
using System.Collections.Generic;
using AttributeMapper.Attributes;

namespace AttributeMapper.Test.Integration.MapFrom.Poco
{
    public class Destination
    {
        [MapFrom("SourceObject")]
        public NestedDestination DestinationObject { get; set; }
        [MapFrom("SourceObjects")]
        public IEnumerable<NestedDestination> DestinationObjects { get; set; }
        [MapFrom("SourceString")]
        public string DestinationString { get; set; }
        [MapFrom("SourceInt")]
        public int DestinationInt { get; set; }
        [MapFrom("SourceEnum")]
        public DestinationEnum DestinationEnum { get; set; }
    }
}