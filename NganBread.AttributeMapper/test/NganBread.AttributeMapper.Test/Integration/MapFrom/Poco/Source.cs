using System.Collections.Generic;

namespace NganBread.AttributeMapper.Test.Net461.Integration.MapFrom.Poco
{
    public class Source
    {
        public NestedSource SourceObject { get; set; }

        public IEnumerable<NestedSource> SourceObjects { get; set; }

        public string SourceString { get; set; }

        public int SourceInt { get; set; }

        public SourceEnum SourceEnum { get; set; }

        public int SourceField;
    }
}
