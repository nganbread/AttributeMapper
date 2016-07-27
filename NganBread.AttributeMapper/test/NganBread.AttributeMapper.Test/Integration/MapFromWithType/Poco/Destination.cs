using NganBread.AttributeMapper.Attributes;

namespace NganBread.AttributeMapper.Test.Net461.Integration.MapFromWithType.Poco
{
    public class Destination
    {
        [MapFrom(nameof(Source.SourceInteger1), MapFromType = typeof(Source))]
        public int DestinationInteger1 { get; set; }

        [MapFrom(nameof(Source.SourceInteger2), MapFromType = typeof(int))]
        public int DestinationInteger2 { get; set; }
    }
}
