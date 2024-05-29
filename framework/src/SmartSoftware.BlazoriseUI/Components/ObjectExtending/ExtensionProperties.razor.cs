using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using SmartSoftware.AspNetCore.Components.Web;
using SmartSoftware.Data;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.BlazoriseUI.Components.ObjectExtending;

public partial class ExtensionProperties<TEntityType, TResourceType> : ComponentBase
    where TEntityType : IHasExtraProperties
{
    [Inject]
    public IStringLocalizerFactory StringLocalizerFactory { get; set; } = default!;

    [Parameter]
    public SmartSoftwareBlazorMessageLocalizerHelper<TResourceType> LH { get; set; } = default!;

    [Parameter]
    public TEntityType Entity { get; set; } = default!;
    
    [Parameter]
    public ExtensionPropertyModalType? ModalType { get; set; }
}
