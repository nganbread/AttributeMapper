using System;

namespace AttributeMapper.Attributes
{
    public class MapFromAttribute : MapAttributeBase
    {
        public MapFromAttribute(string propertyName, Type type = null) 
            : base(propertyName, type)
        {
        }

        public Type MapFromType
        {
            get { return Type; } 
            set { Type = value; }
        }
    }
}
