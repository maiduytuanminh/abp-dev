﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.TextTemplating.Razor.SampleTemplates;
using Xunit;

namespace SmartSoftware.TextTemplating.Razor;

public class SmartSoftwareCompiledViewProviderOptions_Tests : TemplateDefinitionTests<RazorTextTemplatingTestModule>
{
    private readonly ISmartSoftwareCompiledViewProvider _compiledViewProvider;
    private readonly ITemplateDefinitionManager _templateDefinitionManager;

    public SmartSoftwareCompiledViewProviderOptions_Tests()
    {
        _templateDefinitionManager = GetRequiredService<ITemplateDefinitionManager>();
        _compiledViewProvider = GetRequiredService<ISmartSoftwareCompiledViewProvider>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<SmartSoftwareCompiledViewProviderOptions>(options =>
        {
            options.TemplateReferences.Add(RazorTestTemplates.TestTemplate, new List<Assembly>()
                {
                        Assembly.Load("Microsoft.Extensions.Logging.Abstractions")
                }
                .Select(x => MetadataReference.CreateFromFile(x.Location))
                .ToList());
        });
        base.AfterAddApplication(services);
    }

    [Fact]
    public async Task Custom_TemplateReferences_Test()
    {
        var templateDefinition = await _templateDefinitionManager.GetOrNullAsync(RazorTestTemplates.TestTemplate);

        var assembly = await _compiledViewProvider.GetAssemblyAsync(templateDefinition);

        assembly.GetReferencedAssemblies().ShouldContain(x => x.Name == "Microsoft.Extensions.Logging.Abstractions");
    }
}
