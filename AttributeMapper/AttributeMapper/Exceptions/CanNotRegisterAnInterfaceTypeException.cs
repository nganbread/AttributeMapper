using System;

namespace AttributeMapper.Exceptions
{
    internal class CanNotRegisterAnInterfaceTypeException<T> : Exception
    {
        public CanNotRegisterAnInterfaceTypeException()
            :base(String.Format("Interfaces are unable to be registered as they can not be instantiated. {0}", typeof(T)))
        {
            
        }
    }
}
