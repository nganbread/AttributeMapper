using AttributeMapper.Test.Integration.StandardUsingFlexibleTypeMapsTests.Maps;
using AttributeMapper.Test.Integration.StandardUsingFlexibleTypeMapsTests.Poco;
using NUnit.Framework;

namespace AttributeMapper.Test.Integration.StandardUsingFlexibleTypeMapsTests
{
    [TestFixture]
    public class StandardUsingFlexibleTypeMapsTests
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

            AttributeMapper.RegisterMap(new IntToStringFlexibleTypeMap());
            AttributeMapper.RegisterMap(new IntToSubDestinationFlexibleTypeMap());
            _destination = AttributeMapper.Map<Source, Destination>(_source);
        }

        [Test]
        public void GivenThereIsNoImplicitConversion_WhenThereIsASuppliedFlexibleTypeMapperForAValueType_ThenThePropertyIsMapped()
        {
            Assert.AreEqual(IntToStringFlexibleTypeMap.ReturnValue, _destination.Value1);
        }

        [Test]
        public void GivenThereIsNoImplicitConversion_WhenThereIsASuppliedFlexibleTypeMapperForAReferenceType_ThenThePropertyIsMapped()
        {
            Assert.AreEqual(IntToSubDestinationFlexibleTypeMap.ReturnValue, _destination.Value3);
        }

        [Test]
        public void GivenThereIsNoImplicitConversion_WhenThereIsNoSuppliedFlexibleTypeMapper_ThenThePropertyIsNotMapped()
        {
            Assert.AreEqual(default(bool), _destination.Value2);
        }
    }
}
