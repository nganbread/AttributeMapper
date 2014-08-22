using System.Linq;
using AttributeMapper.Attributes;
using AttributeMapper.Core.Contracts;
using Moq;
using NUnit.Framework;

namespace AttributeMapper.Test.Unit.PropertyInfoExtractor
{
    [TestFixture]
    public class ExtractFromProperties
    {
        private Core.MemberInfoExtractor _memberInfoExtractor;
        private Mock<IMemberInfoVerifier> _mockPropertyInfoVerifier;

        [TestFixtureSetUp]
        public void Initialise()
        {
            _mockPropertyInfoVerifier = new Mock<IMemberInfoVerifier>();

            _memberInfoExtractor = new Core.MemberInfoExtractor(_mockPropertyInfoVerifier.Object);
        }

        private class NoProperties { }
        [Test]
        public void WhenAContextHasNoProperties_ThenNoPropertiesAreReturned()
        {
            Assert.AreEqual(0, _memberInfoExtractor.ExtractFromMembers<NoProperties, object>().Count);
        }

        private class OnlyPrivateProperties { private object Property { get; set; }}
        [Test]
        public void WhenAContextHasOnlyPrivateProperties_ThenNoPropertiesAreReturned()
        {
            Assert.AreEqual(0, _memberInfoExtractor.ExtractFromMembers<OnlyPrivateProperties, object>().Count);
        }

        private class PrivateProtectedAndPublicProperties
        {
            protected object Protected { get; set; }
            private object Private { get; set; }
            public object Public { get; set; }
        }
        [Test]
        public void WhenAContextHasPrivateAndPublicProperties_ThenOnlyPublicPropertiesAreReturned()
        {
            Assert.AreEqual(1, _memberInfoExtractor.ExtractFromMembers<PrivateProtectedAndPublicProperties, object>().Count);
        }

        private class InaccessibleProperties
        {
            public object Private { get; private set; }
            public object Protected { get; protected set; }
        }
        [Test]
        public void WhenAContextHasOnlyInaccessibleProperties_ThenNoPropertiesAreReturned()
        {
            Assert.AreEqual(0, _memberInfoExtractor.ExtractFromMembers<OnlyPrivateProperties, object>().Count);
        }

        private class MapFromNameMatchesProperty
        {
            [MapFrom("Public")]
            public object Public { get; set; }
        }
        [Test]
        public void WhenAContextHasAPropertyWithAMatchingMapFromAttribute_Then1PropertyAnd1NameIsReturned()
        {
            var properties = _memberInfoExtractor.ExtractFromMembers<MapFromNameMatchesProperty, object>();
            Assert.AreEqual(1, properties.Count);
            Assert.AreEqual(1, properties.Single().Names.Count);
        }

        private class MapFromDiffersFromProperty
        {
            [MapFrom("Public2")]
            public object Public { get; set; }
        }
        [Test]
        public void WhenAContextHasAPropertyWithADifferentMapFromAttribute_Then1PropertyAnd2NameIsReturned()
        {
            var properties = _memberInfoExtractor.ExtractFromMembers<MapFromDiffersFromProperty, object>();
            Assert.AreEqual(1, properties.Count);
            Assert.AreEqual(2, properties.Single().Names.Count);
        }
    }
}
