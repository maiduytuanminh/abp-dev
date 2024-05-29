namespace SmartSoftware.Cli.Bundling;

internal static class BundlingConsts
{
    internal const string StylePlaceholderStart = "<!--SS:Styles-->";
    internal const string StylePlaceholderEnd = "<!--/SS:Styles-->";
    internal const string ScriptPlaceholderStart = "<!--SS:Scripts-->";
    internal const string ScriptPlaceholderEnd = "<!--/SS:Scripts-->";
    internal const string SupportedWebAssemblyProjectType = "Microsoft.NET.Sdk.BlazorWebAssembly";
    internal const string SupportedMauiBlazorProjectType = "Microsoft.NET.Sdk.Razor";
    internal const string WebAssembly = "webassembly";
    internal const string MauiBlazor = "maui-blazor";
}
