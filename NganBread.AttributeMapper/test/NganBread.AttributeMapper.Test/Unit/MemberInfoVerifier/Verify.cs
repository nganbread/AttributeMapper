using System.Linq;
using Moq;
using NganBread.AttributeMapper.Core.Contracts;
using NganBread.AttributeMapper.Exceptions;
using NUnit.Framework;

namespace NganBread.AttributeMapper.Test.Net461.Unit.MemberInfoVerifier
{
    [TestFixture]
    public class Verify
    {
        private Core.MemberInfoVerifier _memberInfoVerifier;

        [TestFixtureSetUp]
        public void Initialise()
        {
            _memberInfoVerifier = new Core.MemberInfoVerifier();
        }

        [TestCase(new[] { "A" }, new[] { "a" })]
        [TestCase(new[] { "A" }, new[] { "B" }, new[] { "C" })]
        [TestCase(new[] { "A", "A" }, new[] { "B", "B" }, new[] { "C", "C" })]
        [TestCase(new[] { "A1", "A2" }, new[] { "B", "B2" }, new[] { "C", "C2" })]
        public void WhenMembersAreAllUnique_ThenNoExceptionIsThrown(params string[][] memberNames)
        {
            var members = memberNames
                .Select(x =>
                {
                    var mockMember = new Mock<IMemberInfoProxy>();
                    mockMember.Setup(m => m.Names).Returns(x);
                    return mockMember.Object;
                })
                .ToList();
            _memberInfoVerifier.Verify<object>(members);
        }

        [TestCase(new[] { "A" }, new[] { "A" })]
        [TestCase(new[] { "A" }, new[] { "a" }, new[] { "A" })]
        [TestCase(new[] { "A" }, new[] { "A" }, new[] { "A" })]
        public void WhenMembersAreNotAllUnique_ThenAnExceptionIsThrown(params string[][] memberNames)
        {
            var members = memberNames
                .Select(x =>
                {
                    var mockMember = new Mock<IMemberInfoProxy>();
                    mockMember.Setup(m => m.Names).Returns(x);
                    return mockMember.Object;
                })
                .ToList();

            Assert.Catch<DuplicateMemberNamesException<object>>(() => _memberInfoVerifier.Verify<object>(members));
        }
    }
}
