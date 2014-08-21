using AttributeMapper.Attributes;

namespace AttributeMapper.Test.Integration.MapTo.Poco
{
    public class NestedSource
    {
        [MapTo("DestinationString")]
        public string SourceString { get; set; }
        [MapTo("DestinationInt")]
        public int SourceInt { get; set; }
        [MapTo("DestinationEnum")]
        public SourceEnum SourceEnum { get; set; }
    }
}