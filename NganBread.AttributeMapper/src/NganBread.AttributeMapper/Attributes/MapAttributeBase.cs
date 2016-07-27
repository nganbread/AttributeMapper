using System;

namespace NganBread.AttributeMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public abstract class MapAttributeBase : Attribute
    {
        protected MapAttributeBase(string propertyName, Type type = null)
        {
            PropertyName = propertyName;
            Type = type;
        }

        public Type Type { get; protected set; }
        public string PropertyName { get; set; }
    }
}