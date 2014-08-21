using System;

namespace AttributeMapper.Attributes
{
    public class MapFromAttribute : Attribute
    {
        public MapFromAttribute(string propertyName, Type mapFromType = null)
        {
            PropertyName = propertyName;
            MapFromType = mapFromType;
        }

        public string PropertyName { get; private set; }
        public Type MapFromType { get; set; }
    }
}
