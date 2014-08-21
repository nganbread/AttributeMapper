using AttributeMapper.Attributes;

namespace AttributeMapper.Test.Integration.MapToWithType.Poco
{
    public class Source
    {
        [MapTo("DestinationInteger1", MapToType = typeof(Destination))]
        public int SourceInteger1 { get; set; }
        [MapTo("DestinationInteger2", MapToType = typeof(int))]
        public int SourceInteger2 { get; set; }
    }
}
