using System;

namespace AttributeMapper.Attributes
{
    public class MapToAttribute : Attribute
    {
        public MapToAttribute(string propertyName, Type mapToType = null)
        {
            PropertyName = propertyName;
            MapToType = mapToType;
        }

        public string PropertyName { get; private set; }
        public Type MapToType { get; set; }
    }
}