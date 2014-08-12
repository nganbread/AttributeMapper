using AttributeMapper.Maps.Contracts;
using AttributeMapper.TypeConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AttributeMapper.Test.ConverterTests
{
    [TestClass]
    public class SafeConvertTests
    {
        private TypeConverter _typeConverter;

        [TestInitialize]
        public void Init()
        {
            var typeContainer = new Mock<ITypeMapContainer>();
            _typeConverter = new TypeConverter(typeContainer.Object);    
        }

        [TestMethod]
        public void WhenConvertingAStringToAnInt_ThenIntDefaultIsReturned()
        {
            Assert.AreEqual(default(int), _typeConverter.SafeConvert<string, int>(""));
        }

        [TestMethod]
        public void WhenConvertingAnIntToAString_ThenStringDefaultIsReturned()
        {
            Assert.AreEqual(default(string), _typeConverter.SafeConvert<int, string>(5));
        }

        [TestMethod]
        public void WhenConvertingAnIntToADecimal_ThenTheDecimalFormOfTheIntIsReturned()
        {
            Assert.AreEqual(5m, _typeConverter.SafeConvert<int, decimal>(5));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnInt_ThenANonDefaultValueReturned()
        {
            Assert.AreEqual(5, _typeConverter.SafeConvert<A, int>(new A()));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnotherClassType_ThenThatTypeIsReturned()
        {
            Assert.IsInstanceOfType(_typeConverter.SafeConvert<A, B>(new A()), typeof(B));
        }
        [TestMethod]
        public void WhenImplicitlyConvertingToAnotherClassType_ThenANonNullValueIsReturned()
        {
            Assert.IsNotNull(_typeConverter.SafeConvert<A, B>(new A()));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnIncompatibleType_ThenNullIsReturned()
        {
            Assert.IsNull(_typeConverter.SafeConvert<A,C>(new A()));
        }

        [TestMethod]
        public void WhenConvertingNullToAnInt_ThenIntDefaultIsReturned()
        {
            Assert.AreEqual(default(int), _typeConverter.SafeConvert<A, int>(null));
        }

        [TestMethod]
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
