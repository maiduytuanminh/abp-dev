using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.Components;

public partial class DynamicLayoutComponent : ComponentBase
{
    [Inject]
    protected IOptions<SmartSoftwareDynamicLayoutComponentOptions> SmartSoftwareDynamicLayoutComponentOptions { get; set; } = default!;
}