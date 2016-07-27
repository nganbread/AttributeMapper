using System.Collections.Generic;

namespace NganBread.AttributeMapper.Test.Net461.Integration.MapTo.Poco
{
    public class Destination
    {
        public NestedDestination DestinationObject { get; set; }

        public IEnumerable<NestedDestination> DestinationObjects { get; set; }

        public string DestinationString { get; set; }

        public int DestinationInt { get; set; }

        public DestinationEnum DestinationEnum { get; set; }
    }
}