using System;

namespace SmartSoftware.FeatureManagement;

[Serializable]
public class FeatureNameValue : NameValue
{
    public FeatureNameValue()
    {

    }

    public FeatureNameValue(string name, string value)
    {
        Name = name;
        Value = value;
    }
}
