using System;
using System.Collections.Generic;
using System.Linq;
using AttributeMapper.Test.Poco;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AttributeMapper.Test
{
    [TestClass]
    public class MapFromTests
    {
        private Source _source;
        private Destination _destination;

        [TestInitialize]
        public void Initialise()
        {
            _source = new Source
            {
                SourceInt = 1,
                SourceString = "Hello",
                SourceEnum = SourceEnum.B,
                SourceObject = new NestedSource
                {
                    SourceInt = 1,
                    SourceString = "Hello",
                    SourceEnum = SourceEnum.B
                },
                SourceObjects = new List<NestedSource>
                {
                    new NestedSource
                    {
                        SourceInt = 1,
                        SourceString = "Hello",
                        SourceEnum = SourceEnum.B   
                    },
                    new NestedSource
                    {
                        SourceInt = 1,
                        SourceString = "Hello",
                        SourceEnum = SourceEnum.B
                    }
                }
            };

            _destination = AttributeMapper.Map<Destination>(_source);
        }

        [TestMethod]
        public void WhenMappingNull_ThenNullIsReturned()
        {
            Assert.IsNull(AttributeMapper.Map<Destination>(null));
        }

        [TestMethod]
        public void IsNotNull()
        {
            Assert.IsNotNull(_destination);
        }
        [TestMethod]
        public void IsTheCorrectType()
        {
            Assert.IsInstanceOfType(_destination, typeof(Destination));
        }
        [TestMethod]
        public void ObjectIsNotNull()
        {
            Assert.IsNotNull(_destination.DestinationObject);
        }
        [TestMethod]
        public void ObjectIsTheCorrectType()
        {
            Assert.IsInstanceOfType(_destination.DestinationObject, typeof(NestedDestination));
        }

        [TestMethod]
        public void ObjectIntIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceObject.SourceInt, _destination.DestinationObject.DestinationInt);
        }
        [TestMethod]
        public void ObjectEnumIsCorrectlyMapped()
        {
            Assert.AreEqual(DestinationEnum.B, _destination.DestinationObject.DestinationEnum);
        }
        [TestMethod]
        public void ObjectsStringIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceObjects.First().SourceString, _destination.DestinationObjects.First().DestinationString);
        }
        [TestMethod]
        public void ObjectsIntIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceObjects.First().SourceInt, _destination.DestinationObjects.First().DestinationInt);
        }
        [TestMethod]
        public void ObjectsEnumIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceObjects.First().SourceEnum, _destination.DestinationObjects.First().DestinationEnum);
        }
        [TestMethod]
        public void ObjectStringIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceObject.SourceString, _destination.DestinationObject.DestinationString);
        }
        [TestMethod]
        public void ObjectsIsNotNull()
        {
            Assert.IsNotNull(_destination.DestinationObjects);
        }
        [TestMethod]
        public void ObjectsIsTheCorrectType()
        {
            Assert.IsInstanceOfType(_destination.DestinationObjects, typeof(List<NestedDestination>));
        }
        [TestMethod]
        public void ObjectsHasTheCorrectCount()
        {
            Assert.AreEqual(_destination.DestinationObjects.Count(), _source.SourceObjects.Count());
        }
        [TestMethod]
        public void EnumIsCorrectlyMapped()
        {
            Assert.AreEqual(DestinationEnum.B, _destination.DestinationEnum);
        }
        [TestMethod]
        public void IntIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceInt, _destination.DestinationInt);
        }
        [TestMethod]
        public void StringIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.SourceString, _destination.DestinationString);
        }
    }
}
