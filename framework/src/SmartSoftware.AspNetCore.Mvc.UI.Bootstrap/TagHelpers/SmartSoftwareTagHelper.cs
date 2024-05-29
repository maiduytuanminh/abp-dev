using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Threading;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

public abstract class SmartSoftwareTagHelper : TagHelper, ITransientDependency
{

}

public abstract class SmartSoftwareTagHelper<TTagHelper, TService> : SmartSoftwareTagHelper
    where TTagHelper : SmartSoftwareTagHelper<TTagHelper, TService>
    where TService : class, ISmartSoftwareTagHelperService<TTagHelper>
{
    protected TService Service { get; }

    public override int Order => Service.Order;

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = default!;

    protected SmartSoftwareTagHelper(TService service)
    {
        Service = service;
        Service.As<SmartSoftwareTagHelperService<TTagHelper>>().TagHelper = (TTagHelper)this;
    }

    public override void Init(TagHelperContext context)
    {
        Service.Init(context);
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        Service.Process(context, output);
    }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        return Service.ProcessAsync(context, output);
    }
}
