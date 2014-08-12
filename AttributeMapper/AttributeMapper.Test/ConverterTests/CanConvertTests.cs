using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AttributeMapper.Test.ConverterTests
{
    [TestClass]
    public class CanConvertTests
    {
        [TestMethod]
        public void WhenConvertingAStringToAnInt_ThenCanConvert()
        {
            Assert.IsTrue(Converter.CanConvert("", typeof(int)));
        }
        [TestMethod]
        public void WhenConvertingAnIntToAString_ThenCanConvert()
        {
            Assert.IsTrue(Converter.CanConvert(5, typeof(string)));
        }

        [TestMethod]
        public void WhenConvertingAnIntToADecimal_ThenTheDecimalFormOfTheIntIsReturned()
        {
            Assert.IsTrue(Converter.CanConvert(5, typeof(decimal)));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnInt_ThenANonDefaultValueReturned()
        {
            Assert.IsTrue(Converter.CanConvert(new A(), typeof(int)));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnotherClassType_ThenANonNullValueIsReturned()
        {
            Assert.IsTrue(Converter.CanConvert(new A(), typeof(B)));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnIncompatibleType_ThenNullIsReturned()
        {
            Assert.IsFalse(Converter.CanConvert(new A(), typeof(C)));
        }

        public void WhenConvertingNullToAnInt_ThenIntDefaultIsReturned()
        {
            Assert.IsTrue(Converter.CanConvert(null, typeof(int)));
        }

        [TestMethod]
        public void WhenConvertingNullToAClassType_ThenNullIsReturned()
        {
            Assert.IsTrue(Converter.CanConvert(null, typeof(A)));
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
