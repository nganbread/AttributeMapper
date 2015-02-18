using System;
using AttributeMapper.TypeMaps.Contracts;

namespace AttributeMapper.Test.Integration.StandardUsingTypeMapsTests.Maps
{
    public class IntToStringTypeMap : ITypeMap<int, string>
    {
        public static readonly string ReturnValue = "IntToStringMapper";
        public string ConvertCore(int source, Type toType)
        {
            return ReturnValue;
        }
    }
}