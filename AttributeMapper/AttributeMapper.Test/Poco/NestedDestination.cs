using AttributeMapper.Attributes;

namespace AttributeMapper.Test.Poco
{
    public class NestedDestination
    {
        [MapFrom("SourceString")]
        public string DestinationString { get; set; }
        [MapFrom("SourceInt")]
        public int DestinationInt { get; set; }
        [MapFrom("SourceEnum")]
        public DestinationEnum DestinationEnum { get; set; }
    }
}