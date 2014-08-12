﻿using AttributeMapper.Maps.Contracts;
using AttributeMapper.TypeConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AttributeMapper.Test.ConverterTests
{
    [TestClass]
    public class CanConvertTests
    {
        private TypeConverter _typeConverter;

        [TestInitialize]
        public void Init()
        {
            var typeContainer = new Mock<ITypeMapContainer>();
            _typeConverter = new TypeConverter(typeContainer.Object);
        }

        [TestMethod]
        public void WhenConvertingAStringToAnInt_ThenCanConvert()
        {
            Assert.IsTrue(_typeConverter.CanConvert<string, int>(""));
        }
        [TestMethod]
        public void WhenConvertingAnIntToAString_ThenCanConvert()
        {
            Assert.IsTrue(_typeConverter.CanConvert<int, string>(5));
        }

        [TestMethod]
        public void WhenConvertingAnIntToADecimal_ThenTheDecimalFormOfTheIntIsReturned()
        {
            Assert.IsTrue(_typeConverter.CanConvert<int, decimal>(5));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnInt_ThenANonDefaultValueReturned()
        {
            Assert.IsTrue(_typeConverter.CanConvert<A, int>(new A()));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnotherClassType_ThenANonNullValueIsReturned()
        {
            Assert.IsTrue(_typeConverter.CanConvert<A,B>(new A()));
        }

        [TestMethod]
        public void WhenImplicitlyConvertingToAnIncompatibleType_ThenNullIsReturned()
        {
            Assert.IsFalse(_typeConverter.CanConvert<A,C>(new A()));
        }

        public void WhenConvertingNullToAnInt_ThenIntDefaultIsReturned()
        {
            Assert.IsTrue(_typeConverter.CanConvert<A, int>(null));
        }

        [TestMethod]
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
