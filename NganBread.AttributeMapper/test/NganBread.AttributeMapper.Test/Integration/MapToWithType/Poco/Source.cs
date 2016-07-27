using NganBread.AttributeMapper.Attributes;

namespace NganBread.AttributeMapper.Test.Net461.Integration.MapToWithType.Poco
{
    public class Source
    {
        [MapTo(nameof(Destination.DestinationInteger1), MapToType = typeof(Destination))]
        public int SourceInteger1 { get; set; }

        [MapTo(nameof(Destination.DestinationInteger2), MapToType = typeof(int))]
        public int SourceInteger2 { get; set; }
    }
}
