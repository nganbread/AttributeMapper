using System;
using NganBread.AttributeMapper.TypeMaps.Contracts;

namespace NganBread.AttributeMapper.Test.Net461.Integration.StandardUsingTypeMapsTests.Maps
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