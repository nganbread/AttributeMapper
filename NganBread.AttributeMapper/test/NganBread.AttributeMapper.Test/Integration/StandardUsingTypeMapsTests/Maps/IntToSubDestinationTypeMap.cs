using System;
using NganBread.AttributeMapper.Test.Net461.Integration.StandardUsingTypeMapsTests.Poco;
using NganBread.AttributeMapper.TypeMaps.Contracts;

namespace NganBread.AttributeMapper.Test.Net461.Integration.StandardUsingTypeMapsTests.Maps
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