using System;
using AttributeMapper.Test.Integration.StandardUsingTypeMapsTests.Poco;
using AttributeMapper.TypeMaps.Contracts;

namespace AttributeMapper.Test.Integration.StandardUsingTypeMapsTests.Maps
{
    public class IntToSubDestinationTypeMap : ITypeMap<int, SubDestination>
    {
        public static readonly SubDestination ReturnValue = new SubDestination();
        public SubDestination ConvertCore(int source, Type toType)
        {
            return ReturnValue;
        }
    }
}