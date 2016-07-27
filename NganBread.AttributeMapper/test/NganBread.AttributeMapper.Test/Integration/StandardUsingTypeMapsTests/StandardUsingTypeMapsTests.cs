using NganBread.AttributeMapper.Test.Net461.Integration.StandardUsingTypeMapsTests.Maps;
using NganBread.AttributeMapper.Test.Net461.Integration.StandardUsingTypeMapsTests.Poco;
using NUnit.Framework;

namespace NganBread.AttributeMapper.Test.Net461.Integration.StandardUsingTypeMapsTests
{
    [TestFixture]
    public class StandardUsingTypeMapsTests
    {
        private Source _source;
        private Destination _destination;

        [TestFixtureSetUp]
        public void Initialise()
        {
            _source = new Source
            {
                Value1 = 1,
                Value2 = 2
            };

            AttributeMapper.RegisterMap<IntToStringTypeMap, int, string>();
            AttributeMapper.RegisterMap<IntToSubDestinationTypeMap, int, SubDestination>();
            _destination = AttributeMapper.Map<Source, Destination>(_source);
        }

        [Test]
        public void GivenThereIsNoImplicitConversion_WhenThereIsASuppliedTypeMapperForAValueType_ThenThePropertyIsMapped()
        {
            Assert.AreEqual(IntToStringTypeMap.ReturnValue, _destination.Value1);
        }

        [Test]
        public void GivenThereIsNoImplicitConversion_WhenThereIsASuppliedTypeMapperForAReferenceType_ThenThePropertyIsMapped()
        {
            Assert.AreEqual(IntToSubDestinationTypeMap.ReturnValue, _destination.Value3);
        }

        [Test]
        public void GivenThereIsNoImplicitConversion_WhenThereIsNoSuppliedTypeMapper_ThenThePropertyIsNotMapped()
        {
            Assert.AreEqual(default(bool), _destination.Value2);
        }
    }
}
