﻿using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Localization;
using Xunit;

namespace SmartSoftware.TextTemplating;

public class HybridTemplateRendererProvider_Tests : SmartSoftwareTextTemplatingTestBase<SmartSoftwareTextTemplatingTestModule>
{
    private readonly ITemplateRenderer _templateRenderer;

    public HybridTemplateRendererProvider_Tests()
    {
        _templateRenderer = GetRequiredService<ITemplateRenderer>();
    }

    [Fact]
    public async Task Should_Render_By_Scriban()
    {
        using (CultureHelper.Use("en"))
        {
            (await _templateRenderer.RenderAsync(
                TestTemplates.HybridTemplateScriban,
                model: new {
                    name = "John"
                }
            )).ShouldBe("Hello John, how are you?");
        }
    }

    [Fact]
    public async Task Should_Render_By_Razor()
    {
        using (CultureHelper.Use("en"))
        {
            (await _templateRenderer.RenderAsync(
                TestTemplates.HybridTemplateRazor,
                model: new HybridModel
                {
                    Name = "John"
                }
            )).ShouldBe("Hello John, how are you?");
        }
    }

    public class HybridModel
    {
        public string Name { get; set; }
    }

}
