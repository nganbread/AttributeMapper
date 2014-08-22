﻿using System;
using AttributeMapper.Test.Integration.Standard.Poco;
using NUnit.Framework;


namespace AttributeMapper.Test.Integration.Standard
{
    [TestFixture]
    public class StandardTests
    {
        private Source _source;
        private Destination _destination;

        [TestFixtureSetUp]
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

        [Test]
        public void SamePropertyNameAndSameTypeIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.IntegerWillMapToInteger, _destination.IntegerWillMapToInteger);
        }

        [Test]
        public void SamePropertyNameAndDifferentTypeWontMap()
        {
            Assert.AreEqual(default(Guid), _destination.IntegerWontMapToGuid);
        }

        [Test]
        public void SamePropertyNameAndImplicitlyCompatibleTypeIsCorrectlyMapped()
        {
            Assert.AreEqual(_source.IntegerWillImplicitlyMapToDouble, _destination.IntegerWillImplicitlyMapToDouble);
        }
    }
}
