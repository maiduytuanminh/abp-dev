using System;
using SmartSoftware.Modularity;
using SmartSoftware.TextTemplating.Scriban;

namespace SmartSoftware.TextTemplating;

[Obsolete("This module will be removed in the future. Please use SmartSoftwareTextTemplatingScribanModule or SmartSoftwareTextTemplatingRazorModule.")]
[DependsOn(typeof(SmartSoftwareTextTemplatingScribanModule))]
public class SmartSoftwareTextTemplatingModule : SmartSoftwareModule
{

}
