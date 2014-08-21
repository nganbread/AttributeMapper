using AttributeMapper.Attributes;

namespace AttributeMapper.Test.Integration.MapFromWithType.Poco
{
    public class Destination
    {
        [MapFrom("SourceInteger1", MapFromType=typeof(Source))]
        public int DestinationInteger1 { get; set; }

        [MapFrom("SourceInteger2", MapFromType = typeof(int))]
        public int DestinationInteger2 { get; set; }
    }
}
