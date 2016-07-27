using System;
using Moq;
using NganBread.AttributeMapper.Exceptions;
using NganBread.AttributeMapper.TypeMaps;
using NganBread.AttributeMapper.TypeMaps.Contracts;
using NUnit.Framework;

namespace NganBread.AttributeMapper.Test.Net461.Unit.TypeMapContainer
{
    [TestFixture]
    public class RegisterMap
    {
        private Mock<TypeMapAdapterFactory> _typeMapAdapterFactory;
        private Core.TypeMapContainer _typeMapContainer;

        [OneTimeSetUp]
        public void Initialise()
        {
            _typeMapAdapterFactory = new Mock<TypeMapAdapterFactory>();
            _typeMapContainer = new Core.TypeMapContainer(_typeMapAdapterFactory.Object);
        }

        private class TypeMap : ITypeMap<object, object>
        {
            public object ConvertCore(object source, Type toType)
            {
                throw new NotImplementedException();    
            }
        }

        [Test]
        public void WhenRegisteringACompatibleType_ThenTheTypeMapCanBeResolved()
        {
            _typeMapContainer.RegisterMap<TypeMap, object, object>();
        }

        private abstract class AbstractTypeMap : ITypeMap<object, object>
        {
            public abstract object ConvertCore(object source, Type toType);
        }

        [Test]
        public void WhenRegisteringAnAbstractTypeMap_ThenAnExceptionIsThrown()
        {
            Assert.Throws<CanNotRegisterAnAbstractTypeException<AbstractTypeMap>>(() =>
            {
                _typeMapContainer.RegisterMap<AbstractTypeMap, object, object>();
            });
        }


        private interface InterfaceTypeMap : ITypeMap<object, object>
        {
            
        }
        [Test]
        public void WhenRegisteringAnInterfaceTypeMap_ThenAnExceptionIsThrown()
        {
            Assert.Throws<CanNotRegisterAnInterfaceTypeException<InterfaceTypeMap>>(() =>
            {
                _typeMapContainer.RegisterMap<InterfaceTypeMap, object, object>();
            });
        }
    }
}
