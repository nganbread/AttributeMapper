using System;
using AttributeMapper.Test.Integration.StandardUsingFlexibleTypeMapsTests.Poco;
using AttributeMapper.TypeMaps.Contracts;

namespace AttributeMapper.Test.Integration.StandardUsingFlexibleTypeMapsTests.Maps
{
    public class IntToSubDestinationFlexibleTypeMap : IFlexibleTypeMap
    {
        public static readonly SubDestination ReturnValue = new SubDestination();
        public object ConvertCore(object source, Type toType)
        {
            return ReturnValue;
        }

        public bool CanConvert(Type fromType, Type toType)
        {
            return
                fromType == typeof (int) &&
                toType == typeof(SubDestination);
        }
    }
}