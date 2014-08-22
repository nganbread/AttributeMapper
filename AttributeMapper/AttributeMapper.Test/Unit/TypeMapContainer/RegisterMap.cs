using System;
using AttributeMapper.Exceptions;
using AttributeMapper.TypeMaps;
using AttributeMapper.TypeMaps.Contracts;
using Moq;
using NUnit.Framework;

namespace AttributeMapper.Test.Unit.TypeMapContainer
{
    [TestFixture]
    public class RegisterMap
    {
        private Mock<TypeMapAdapterFactory> _typeMapAdapterFactory;
        private Core.TypeMapContainer _typeMapContainer;

        [TestFixtureSetUp]
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
        [ExpectedException(typeof(CanNotRegisterAnAbstractTypeException<AbstractTypeMap>))]
        public void WhenRegisteringAnAbstractTypeMap_ThenAnExceptionIsThrown()
        {
            _typeMapContainer.RegisterMap<AbstractTypeMap, object, object>();
        }


        private interface InterfaceTypeMap : ITypeMap<object, object>
        {
            
        }
        [Test]
        [ExpectedException(typeof (CanNotRegisterAnInterfaceTypeException<InterfaceTypeMap>))]
        public void WhenRegisteringAnInterfaceTypeMap_ThenAnExceptionIsThrown()
        {
            _typeMapContainer.RegisterMap<InterfaceTypeMap, object, object>();
        }
    }
}
