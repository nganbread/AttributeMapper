using System;
using NganBread.AttributeMapper.Test.Net461.Integration.StandardUsingFlexibleTypeMapsTests.Poco;
using NganBread.AttributeMapper.TypeMaps.Contracts;

namespace NganBread.AttributeMapper.Test.Net461.Integration.StandardUsingFlexibleTypeMapsTests.Maps
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