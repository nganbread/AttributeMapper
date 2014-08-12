using System;

namespace AttributeMapper.Attributes
{
    public class MapFromAttribute : Attribute
    {
        public string PropertyName { get; private set; }

        public MapFromAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
