using System;

namespace AttributeMapper.Attributes
{
    internal class MapToAttribute : MapAttributeBase
    {
        public MapToAttribute(string propertyName, Type type = null) 
            : base(propertyName, type)
        {
        }

        public Type MapToType
        {
            get { return Type; }
            set { Type = value; }
        }
    }
}