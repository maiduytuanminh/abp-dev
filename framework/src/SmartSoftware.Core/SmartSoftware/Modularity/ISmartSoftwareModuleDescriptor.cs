﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace SmartSoftware.Modularity;

public interface ISmartSoftwareModuleDescriptor
{
    /// <summary>
    /// Type of the module class.
    /// </summary>
    Type Type { get; }

    /// <summary>
    /// Main assembly that defines the module <see cref="Type"/>.
    /// </summary>
    Assembly Assembly { get; }
    
    /// <summary>
    /// All the assemblies of the module.
    /// Includes the main <see cref="Assembly"/> and other assemblies defined
    /// on the module <see cref="Type"/> using the <see cref="AdditionalAssemblyAttribute"/> attribute.
    /// </summary>
    Assembly[] AllAssemblies { get; }

    /// <summary>
    /// The instance of the module class (singleton).
    /// </summary>
    ISmartSoftwareModule Instance { get; }

    /// <summary>
    /// Is this module loaded as a plug-in?
    /// </summary>
    bool IsLoadedAsPlugIn { get; }

    /// <summary>
    /// Modules on which this module depends on.
    /// A module can depend on another module using the <see cref="DependsOnAttribute"/> attribute.
    /// </summary>
    IReadOnlyList<ISmartSoftwareModuleDescriptor> Dependencies { get; }
}