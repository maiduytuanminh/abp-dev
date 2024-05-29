// This file is automatically generated by SS framework to use MVC Controllers from CSharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware;
using SmartSoftware.Application.Dtos;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Http.Client.ClientProxying;
using SmartSoftware.Http.Modeling;
using SmartSoftware.CmsKit.Admin.Blogs;
using SmartSoftware.CmsKit.Blogs;

// ReSharper disable once CheckNamespace
namespace SmartSoftware.CmsKit.Admin.Blogs;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IBlogFeatureAdminAppService), typeof(BlogFeatureAdminClientProxy))]
public partial class BlogFeatureAdminClientProxy : ClientProxyBase<IBlogFeatureAdminAppService>, IBlogFeatureAdminAppService
{
    public virtual async Task<List<BlogFeatureDto>> GetListAsync(Guid blogId)
    {
        return await RequestAsync<List<BlogFeatureDto>>(nameof(GetListAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), blogId }
        });
    }

    public virtual async Task SetAsync(Guid blogId, BlogFeatureInputDto dto)
    {
        await RequestAsync(nameof(SetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), blogId },
            { typeof(BlogFeatureInputDto), dto }
        });
    }
}
