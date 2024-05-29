using System;
using System.Collections.Generic;
using SmartSoftware.Collections;

namespace SmartSoftware.Validation;

public class SmartSoftwareValidationOptions
{
    public List<Type> IgnoredTypes { get; }

    public ITypeList<IObjectValidationContributor> ObjectValidationContributors { get; set; }

    public SmartSoftwareValidationOptions()
    {
        IgnoredTypes = new List<Type>();
        ObjectValidationContributors = new TypeList<IObjectValidationContributor>();
    }
}
