using Moq;
using NganBread.AttributeMapper.Core.Contracts;
using NUnit.Framework;

namespace NganBread.AttributeMapper.Test.Net461.Unit.TypeConverter
{
    [TestFixture]
    public class CanConvert
    {
        private Core.TypeConverter _typeConverter;

        [OneTimeSetUp]
        public void Init()
        {
            var typeContainer = new Mock<ITypeMapContainer>();
            _typeConverter = new Core.TypeConverter(typeContainer.Object);
        }
        
        [Test]
        public void WhenConvertingAStringToAnInt_ThenCanConvert()
        {
            Assert.IsTrue(_typeConverter.CanConvert<string, int>(""));
        }

        [Test]
        public void WhenConvertingAnIntToAString_ThenCanConvert()
        {
            Assert.IsTrue(_typeConverter.CanConvert<int, string>(5));
        }

        [Test]
        public void WhenConvertingAnIntToADecimal_ThenTheDecimalFormOfTheIntIsReturned()
        {
            Assert.IsTrue(_typeConverter.CanConvert<int, decimal>(5));
        }

        [Test]
        public void WhenImplicitlyConvertingToAnInt_ThenANonDefaultValueReturned()
        {
            Assert.IsTrue(_typeConverter.CanConvert<A, int>(new A()));
        }

        [Test]
        public void WhenImplicitlyConvertingToAnotherClassType_ThenANonNullValueIsReturned()
        {
            Assert.IsTrue(_typeConverter.CanConvert<A,B>(new A()));
        }

        [Test]
        public void WhenImplicitlyConvertingToAnIncompatibleType_ThenNullIsReturned()
        {
            Assert.IsFalse(_typeConverter.CanConvert<A,C>(new A()));
        }

        [Test]
        public void WhenConvertingNullToAnInt_ThenIntDefaultIsReturned()
        {
            Assert.IsTrue(_typeConverter.CanConvert<A, int>(null));
        }

        [Test]
        public void WhenConvertingNullToAClassType_ThenNullIsReturned()
        {
            Assert.IsTrue(_typeConverter.CanConvert<A,A>(null));
        }

        public class A
        {
            public static implicit operator int(A a)
            {
                return 5;
            }
            public static implicit operator B(A a)
            {
                return new B();
            }
        }
        public class B
        {
        }

        public class C
        {

        }
    }
}
