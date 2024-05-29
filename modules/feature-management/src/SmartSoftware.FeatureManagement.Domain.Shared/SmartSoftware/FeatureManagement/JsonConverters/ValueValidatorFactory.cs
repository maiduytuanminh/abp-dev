﻿using SmartSoftware.Validation.StringValues;

namespace SmartSoftware.FeatureManagement.JsonConverters;

public class ValueValidatorFactory<TValueValidator> : IValueValidatorFactory
    where TValueValidator : IValueValidator, new()
{
    protected readonly string Name;
    
    public ValueValidatorFactory(string name)
    {
        Name = name;
    }
    
    public bool CanCreate(string name)
    {
        return Name == name;
    }

    public IValueValidator Create()
    {
        return new TValueValidator() as IValueValidator;
    }
}