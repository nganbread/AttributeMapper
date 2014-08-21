using System;
using AttributeMapper.Test.Integration.Standard.Poco;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AttributeMapper.Test.Integration.Standard
{
    [TestClass]
    public class StandardTests
    {
        private Source _source;
        private Destination _destination;

        [TestInitialize]
        public void Initialise()
        {
            _source = new Source
            {
                DifferingName2 = 1,
                IntegerWontMapToGuid = 2,
                IntegerWillMapToInteger = 3,
                IntegerWillImplicitlyMapToDouble = 4
            };

            _destination = AttributeMapper.Map<Source, Destination>(_source);
        }

        [TestMethod]
        public void SamePropertyNameAndSameTypeIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.IntegerWillMapToInteger, _destination.IntegerWillMapToInteger);
        }

        [TestMethod]
        public void SamePropertyNameAndDifferentTypeWontMap()
        {
            Assert.AreEqual(default(Guid), _destination.IntegerWontMapToGuid);
        }

        [TestMethod]
        public void SamePropertyNameAndImplicitlyCompatibleTypeIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.IntegerWillImplicitlyMapToDouble, _destination.IntegerWillImplicitlyMapToDouble);
        }
    }
}
