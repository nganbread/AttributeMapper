using NganBread.AttributeMapper.Attributes;

namespace NganBread.AttributeMapper.Test.Net461.Integration.MapFrom.Poco
{
    public class NestedDestination
    {
        [MapFrom(nameof(NestedSource.SourceString))]
        public string DestinationString { get; set; }

        [MapFrom(nameof(NestedSource.SourceInt))]
        public int DestinationInt { get; set; }

        [MapFrom(nameof(NestedSource.SourceEnum))]
        public DestinationEnum DestinationEnum { get; set; }
    }
}