﻿using System;

namespace AttributeMapper.Test.Integration.Standard.Poco
{
    public class Destination
    {
        public int IntegerWillMapToInteger { get; set; }
        public Guid IntegerWontMapToGuid { get; set; }
        public double IntegerWillImplicitlyMapToDouble { get; set; }
        public int DifferingName1 { get; set; }
    }
}
