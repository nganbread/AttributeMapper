using AttributeMapper.Test.Integration.MapFromWithType.Poco;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AttributeMapper.Test.Integration.MapFromWithType
{
    [TestClass]
    public class MapFromWithTypeTests
    {
        private Source _source;
        private Destination _destination;

        [TestInitialize]
        public void Initialise()
        {
            _source = new Source
            {
                SourceInteger1 = 1,
                SourceInteger2 = 2
            };

            _destination = AttributeMapper.Map<Source, Destination>(_source);
        }

        [TestMethod]
        public void WhenTheSourceTypeMatches_ThenThePropertyIsMapped()
        {
            Assert.AreEqual(_source.SourceInteger1, _destination.DestinationInteger1);
        }

        [TestMethod]
        public void WhenTheSourceTypeDoesNotMatch_ThenThePropertyIsNotMapped()
        {
            Assert.AreEqual(default(int), _destination.DestinationInteger2);
        }
    }
}
