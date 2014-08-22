using System.Linq;
using AttributeMapper.Attributes;
using AttributeMapper.Core.Contracts;
using Moq;
using NUnit.Framework;

namespace AttributeMapper.Test.Unit.MemberInfoExtractor
{
    [TestFixture]
    public class ExtractFromMember
    {
        private Core.MemberInfoExtractor _memberInfoExtractor;
        private Mock<IMemberInfoVerifier> _mockMemberInfoVerifier;

        [TestFixtureSetUp]
        public void Initialise()
        {
            _mockMemberInfoVerifier = new Mock<IMemberInfoVerifier>();

            _memberInfoExtractor = new Core.MemberInfoExtractor(_mockMemberInfoVerifier.Object);
        }

        private class NoMembers { }
        [Test]
        public void WhenAContextHasNoMembers_ThenNoMembersAreReturned()
        {
            Assert.AreEqual(0, _memberInfoExtractor.ExtractFromMembers<NoMembers, object>().Count);
        }

        private class PrivateProtectedAndPublicMembers
        {
            protected object ProtectedProperty { get; set; }
            private object PrivateProperty { get; set; }
            public object PublicProperty { get; set; }

            protected object ProtectedField;
            private object PrivateField;
            public object PublicField;
        }
        [Test]
        public void WhenAContextHasPrivateProtectedAndPublicMembers_ThenOnlyPublicMembersAreReturned()
        {
            Assert.AreEqual(2, _memberInfoExtractor.ExtractFromMembers<PrivateProtectedAndPublicMembers, object>().Count);
        }

        private class InaccessibleMembers
        {
            public object Private { get; private set; }
            public object Protected { get; protected set; }
            public readonly object Field;
        }
        [Test]
        public void WhenAContextHasOnlyInaccessibleMembers_ThenNoMembersAreReturned()
        {
            Assert.AreEqual(0, _memberInfoExtractor.ExtractFromMembers<InaccessibleMembers, object>().Count);
        }

        private class MatchingNamesArentDuplicated
        {
            [MapFrom("PublicProperty")]
            public object PublicProperty { get; set; }

            [MapFrom("PublicField")] 
            public object PublicField;
        }
        [Test]
        public void WhenAContextHasAPropertyWithAMatchingMapFromAttribute_ThenADistinctListIsReturned()
        {
            var properties = _memberInfoExtractor.ExtractFromMembers<MatchingNamesArentDuplicated, object>();
            Assert.AreEqual(2, properties.Count);
            Assert.AreEqual(1, properties.First().Names.Count);
            Assert.AreEqual(1, properties.Last().Names.Count);
        }

        private class MapFromNameIsDifferent
        {
            [MapFrom("PublicProperty2")]
            public object PublicProperty { get; set; }
            [MapFrom("PublicField2")]
            public object PublicField;
        }
        [Test]
        public void WhenAContextHasMembersWithADifferentMapFromAttribute_ThenBothNamesAreReturned()
        {
            var properties = _memberInfoExtractor.ExtractFromMembers<MapFromNameIsDifferent, object>();
            Assert.AreEqual(2, properties.Count);
            Assert.AreEqual(2, properties.First().Names.Count);
            Assert.AreEqual(2, properties.Last().Names.Count);
        }
    }
}
