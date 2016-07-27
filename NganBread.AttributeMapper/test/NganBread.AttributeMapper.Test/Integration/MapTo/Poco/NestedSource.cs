using NganBread.AttributeMapper.Attributes;

namespace NganBread.AttributeMapper.Test.Net461.Integration.MapTo.Poco
{
    public class NestedSource
    {
        [MapTo(nameof(Destination.DestinationString))]
        public string SourceString { get; set; }

        [MapTo(nameof(Destination.DestinationInt))]
        public int SourceInt { get; set; }

        [MapTo(nameof(Destination.DestinationEnum))]
        public SourceEnum SourceEnum { get; set; }
    }
}