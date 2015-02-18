using System;
using AttributeMapper.TypeMaps.Contracts;

namespace AttributeMapper.Test.Integration.StandardUsingFlexibleTypeMapsTests.Maps
{
    public class IntToStringFlexibleTypeMap : IFlexibleTypeMap
    {
        public static readonly string ReturnValue = "IntToStringFlexibleTypeMap";
        public object ConvertCore(object source, Type toType)
        {
            return ReturnValue;
        }

        public bool CanConvert(Type fromType, Type toType)
        {
            return
                fromType == typeof (int) &&
                toType == typeof (string);
        }
    }
}