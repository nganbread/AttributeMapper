using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AttributeMapper.Test.ConverterTests
{
    [TestClass]
    public class SafeConvertTests
    {
        [TestMethod]
        public void WhenConvertingAStringToAnInt_ThenIntDefaultIsReturned()
        {
            Assert.AreEqual(default(int), Converter.SafeConvert("", typeof(int)));
        }
        [TestMethod]
        public void WhenConvertingAnIntToAString_ThenStringDefaultIsReturned()
        {
            Assert.AreEqual(default(string), Converter.SafeConvert(5, typeof(string)));
        }
        [TestMethod]
        public void WhenConvertingAnIntToADecimal_ThenTheDecimalFormOfTheIntIsReturned()
        {
            Assert.AreEqual((decimal)5, Converter.SafeConvert(5, typeof(decimal)));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnInt_ThenANonDefaultValueReturned()
        {
            Assert.AreEqual(5, Converter.SafeConvert(new A(), typeof(int)));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnotherClassType_ThenThatTypeIsReturned()
        {
            Assert.IsInstanceOfType(Converter.SafeConvert(new A(), typeof(B)), typeof(B));
        }
        [TestMethod]
        public void WhenImplicitlyConvertingToAnotherClassType_ThenANonNullValueIsReturned()
        {
            Assert.IsNotNull(Converter.SafeConvert(new A(), typeof(B)));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnIncompatibleType_ThenNullIsReturned()
        {
            Assert.IsNull(Converter.SafeConvert(new A(), typeof(C)));
        }

        [TestMethod]
        public void WhenConvertingNullToAnInt_ThenIntDefaultIsReturned()
        {
            Assert.AreEqual(default(int), Converter.SafeConvert(null, typeof(int)));
        }

        [TestMethod]
        public void WhenConvertingNullToAClassType_ThenNullIsReturned()
        {
            Assert.IsNull(Converter.SafeConvert(null, typeof(A)));
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
