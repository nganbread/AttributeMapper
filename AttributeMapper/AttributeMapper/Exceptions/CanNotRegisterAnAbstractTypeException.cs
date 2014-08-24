using System;

namespace AttributeMapper.Exceptions
{
    internal class CanNotRegisterAnAbstractTypeException<T> : Exception
    {
        public CanNotRegisterAnAbstractTypeException()
            :base(String.Format("Abstract types are unable to be registered as they can not be instantiated. {0}", typeof(T)))
        {
            
        }
    }
}