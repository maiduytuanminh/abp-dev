﻿using System;
using SmartSoftware.Reflection;
using SmartSoftware.Threading;

namespace SmartSoftware.Http.Modeling;

[Serializable]
public class ReturnValueApiDescriptionModel
{
    public string Type { get; set; } = default!;

    public string TypeSimple { get; set; } = default!;

    public ReturnValueApiDescriptionModel()
    {

    }

    public static ReturnValueApiDescriptionModel Create(Type type)
    {
        var unwrappedType = AsyncHelper.UnwrapTask(type);

        return new ReturnValueApiDescriptionModel
        {
            Type = TypeHelper.GetFullNameHandlingNullableAndGenerics(unwrappedType),
            TypeSimple = ApiTypeNameHelper.GetSimpleTypeName(unwrappedType)
        };
    }
}
