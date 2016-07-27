using System.Collections.Generic;
using System.Linq;
using NganBread.AttributeMapper.Test.Net461.Integration.MapTo.Poco;
using NUnit.Framework;

namespace NganBread.AttributeMapper.Test.Net461.Integration.MapTo
{
    [TestFixture]
    public class MapToTests
    {
        private Source _source;
        private Destination _destination;

        [OneTimeSetUp]
        public void Initialise()
        {
            _source = new Source
            {
                SourceInt = 1,
                SourceString = "1",
                SourceEnum = SourceEnum.A,
                SourceObject = new NestedSource
                {
                    SourceInt = 2,
                    SourceString = "2",
                    SourceEnum = SourceEnum.B
                },
                SourceObjects = new List<NestedSource>
                {
                    new NestedSource
                    {
                        SourceInt = 3,
                        SourceString = "3",
                        SourceEnum = SourceEnum.C
                    },
                    new NestedSource
                    {
                        SourceInt = 4,
                        SourceString = "4",
                        SourceEnum = SourceEnum.D
                    }
                }
            };

            _destination = AttributeMapper.Map<Source, Destination>(_source);
        }

        [Test]
        public void WhenMappingNull_ThenNullIsReturned()
        {
            Assert.IsNull(AttributeMapper.Map<Source, Destination>(null));
        }

        [Test]
        public void IsNotNull()
        {
            Assert.IsNotNull(_destination);
        }

        [Test]
        public void IsTheCorrectType()
        {
            Assert.IsInstanceOf<Destination>(_destination);
        }

        [Test]
        public void ObjectIsNotNull()
        {
            Assert.IsNotNull(_destination.DestinationObject);
        }

        [Test]
        public void ObjectIsTheCorrectType()
        {
            Assert.IsInstanceOf<NestedDestination>(_destination.DestinationObject);
        }

        [Test]
        public void ObjectIntIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceObject.SourceInt, _destination.DestinationObject.DestinationInt);
        }

        [Test]
        public void ObjectEnumIsCorrectlyMapped()
        {
            Assert.AreEqual(DestinationEnum.B, _destination.DestinationObject.DestinationEnum);
        }

        [Test]
        public void ObjectsStringIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceObjects.First().SourceString,
                _destination.DestinationObjects.First().DestinationString);
        }

        [Test]
        public void ObjectsIntIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceObjects.First().SourceInt,_destination.DestinationObjects.First().DestinationInt);
        }

        [Test]
        public void ObjectsEnumIsCorrectlyMapped()
        {
            Assert.AreEqual(
                (int)_source.SourceObjects.First().SourceEnum,
                (int)_destination.DestinationObjects.First().DestinationEnum);
        }

        [Test]
        public void ObjectStringIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceObject.SourceString, _destination.DestinationObject.DestinationString);
        }

        [Test]
        public void ObjectsIsNotNull()
        {
            Assert.IsNotNull(_destination.DestinationObjects);
        }

        [Test]
        public void ObjectsIsTheCorrectType()
        {
            Assert.IsInstanceOf<List<NestedDestination>>(_destination.DestinationObjects);
        }

        [Test]
        public void ObjectsHasTheCorrectCount()
        {
            Assert.AreEqual(_destination.DestinationObjects.Count(), _source.SourceObjects.Count());
        }

        [Test]
        public void EnumIsCorrectlyMapped()
        {
            Assert.AreEqual(DestinationEnum.A, _destination.DestinationEnum);
        }

        [Test]
        public void IntIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceInt, _destination.DestinationInt);
        }

        [Test]
        public void StringIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceString, _destination.DestinationString);
        }
    }
}
