﻿using JetBrains.Annotations;

namespace SmartSoftware.ObjectExtending.Modularity;

public class ExtensionPropertyUiConfiguration
{
    public const int DefaultOrder = 1000;
    
    [NotNull]
    public ExtensionPropertyUiTableConfiguration OnTable { get; }

    [NotNull]
    public ExtensionPropertyUiFormConfiguration OnCreateForm { get; }

    [NotNull]
    public ExtensionPropertyUiFormConfiguration OnEditForm { get; }

    [NotNull]
    public ExtensionPropertyLookupConfiguration Lookup { get; set; }
    
    public int Order { get; set; }

    public ExtensionPropertyUiConfiguration()
    {
        OnTable = new ExtensionPropertyUiTableConfiguration();
        OnCreateForm = new ExtensionPropertyUiFormConfiguration();
        OnEditForm = new ExtensionPropertyUiFormConfiguration();
        Lookup = new ExtensionPropertyLookupConfiguration();
        Order = DefaultOrder;
    }
}
