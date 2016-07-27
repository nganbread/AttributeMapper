using System;
using NganBread.AttributeMapper.TypeMaps.Contracts;

namespace NganBread.AttributeMapper.Test.Net461.Integration.StandardUsingFlexibleTypeMapsTests.Maps
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