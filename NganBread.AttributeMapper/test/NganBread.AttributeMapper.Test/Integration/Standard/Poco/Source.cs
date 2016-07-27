namespace NganBread.AttributeMapper.Test.Net461.Integration.Standard.Poco
{
    public class Source
    {
        public int IntegerWillMapToInteger { get; set; }

        public int IntegerWontMapToGuid { get; set; }

        public int IntegerWillImplicitlyMapToDouble { get; set; }

        public int DifferingName2 { get; set; }
    }
}
