using System.Linq;
using AttributeMapper.Core;
using AttributeMapper.Exceptions;
using Moq;
using NUnit.Framework;

namespace AttributeMapper.Test.Unit.PropertyInfoVerifier
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
        public void WhenPropertiesAreAllUnique_ThenNoExceptionIsThrown(params string[][] propertyNames)
        {
            var properties = propertyNames
                .Select(x =>
                {
                    var mockProperty = new Mock<IMemberInfoProxy>();
                    mockProperty.Setup(m => m.Names).Returns(x);
                    return mockProperty.Object;
                })
                .ToList();
            _memberInfoVerifier.Verify<object>(properties);
        }

        [TestCase(new[] { "A" }, new[] { "A" })]
        [TestCase(new[] { "A" }, new[] { "a" }, new[] { "A" })]
        [TestCase(new[] { "A" }, new[] { "A" }, new[] { "A" })]
        public void WhenPropertiesAreNotAllUnique_ThenAnExceptionIsThrown(params string[][] propertyNames)
        {
            var properties = propertyNames
                .Select(x =>
                {
                    var mockProperty = new Mock<IMemberInfoProxy>();
                    mockProperty.Setup(m => m.Names).Returns(x);
                    return mockProperty.Object;
                })
                .ToList();

            Assert.Catch<DuplicatePropertyNamesException<object>>(() => _memberInfoVerifier.Verify<object>(properties));
        }
    }
}
