using Microsoft.AspNetCore.Cors.Infrastructure;
using SmartSoftware.Http;
namespace Microsoft.AspNetCore.Cors;

public static class SmartSoftwareCorsPolicyBuilderExtensions
{
    public static CorsPolicyBuilder WithSmartSoftwareExposedHeaders(this CorsPolicyBuilder corsPolicyBuilder)
    {
        return corsPolicyBuilder.WithExposedHeaders(SmartSoftwareHttpConsts.SmartSoftwareErrorFormat).WithExposedHeaders(SmartSoftwareHttpConsts.SmartSoftwareTenantResolveError);
    }
}
