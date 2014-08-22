using AttributeMapper.Core.Contracts;
using Moq;
using NUnit.Framework;


namespace AttributeMapper.Test.Unit.TypeConverter
{
    [TestFixture]
    public class SafeConvertTests
    {
        private Core.TypeConverter _typeConverter;

        [TestFixtureSetUp]
        public void Init()
        {
            var typeContainer = new Mock<ITypeMapContainer>();
            _typeConverter = new Core.TypeConverter(typeContainer.Object);    
        }

        [Test]
        public void WhenConvertingAStringToAnInt_ThenIntDefaultIsReturned()
        {
            Assert.AreEqual(default(int), _typeConverter.SafeConvert<string, int>(""));
        }

        [Test]
        public void WhenConvertingAnIntToAString_ThenStringDefaultIsReturned()
        {
            Assert.AreEqual(default(string), _typeConverter.SafeConvert<int, string>(5));
        }

        [Test]
        public void WhenConvertingAnIntToADecimal_ThenTheDecimalFormOfTheIntIsReturned()
        {
            Assert.AreEqual(5m, _typeConverter.SafeConvert<int, decimal>(5));
        }

        [Test]
        public void WhenImplicitlyConvertingToAnInt_ThenANonDefaultValueReturned()
        {
            Assert.AreEqual(5, _typeConverter.SafeConvert<A, int>(new A()));
        }

        [Test]
        public void WhenImplicitlyConvertingToAnotherClassType_ThenThatTypeIsReturned()
        {
            Assert.IsInstanceOf<B>(_typeConverter.SafeConvert<A, B>(new A()));
        }
        [Test]
        public void WhenImplicitlyConvertingToAnotherClassType_ThenANonNullValueIsReturned()
        {
            Assert.IsNotNull(_typeConverter.SafeConvert<A, B>(new A()));
        }

        [Test]
        public void WhenImplicitlyConvertingToAnIncompatibleType_ThenNullIsReturned()
        {
            Assert.IsNull(_typeConverter.SafeConvert<A,C>(new A()));
        }

        [Test]
        public void WhenConvertingNullToAnInt_ThenIntDefaultIsReturned()
        {
            Assert.AreEqual(default(int), _typeConverter.SafeConvert<A, int>(null));
        }

        [Test]
        public void WhenConvertingNullToAClassType_ThenNullIsReturned()
        {
            Assert.IsNull(_typeConverter.SafeConvert<A,A>(null));
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
