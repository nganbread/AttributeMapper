﻿using System;

namespace AttributeMapper.Maps.Contracts
{
    public interface ITypeMap<in TFrom, out TTo>
    {
        TTo ConvertCore(TFrom source, Type toType);
    }
}