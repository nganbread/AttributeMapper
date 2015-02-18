using System;
using AttributeMapper.Test.Integration.MapFromWithType.Poco;
using AttributeMapper.TypeMaps.Contracts;
using NUnit.Framework;


namespace AttributeMapper.Test.Integration.MapFromWithType
{
    [TestFixture]
    public class MapFromWithTypeTests
    {
        private Source _source;
        private Destination _destination;

        [TestFixtureSetUp]
        public void Initialise()
        {
            _source = new Source
            {
                SourceInteger1 = 1,
                SourceInteger2 = 2
            };

            _destination = AttributeMapper.Map<Source, Destination>(_source);
        }

        [Test]
        public void WhenTheSourceTypeMatches_ThenThePropertyIsMapped()
        {
            Assert.AreEqual(_source.SourceInteger1, _destination.DestinationInteger1);
        }

        [Test]
        public void WhenTheSourceTypeDoesNotMatch_ThenThePropertyIsNotMapped()
        {
            Assert.AreEqual(default(int), _destination.DestinationInteger2);
        }
    }
}
