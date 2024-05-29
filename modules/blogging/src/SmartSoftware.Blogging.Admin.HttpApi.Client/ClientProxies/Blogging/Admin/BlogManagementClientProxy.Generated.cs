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
using SmartSoftware.Blogging.Admin.Blogs;
using SmartSoftware.Blogging.Blogs.Dtos;

// ReSharper disable once CheckNamespace
namespace SmartSoftware.Blogging.Admin;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IBlogManagementAppService), typeof(BlogManagementClientProxy))]
public partial class BlogManagementClientProxy : ClientProxyBase<IBlogManagementAppService>, IBlogManagementAppService
{
    public virtual async Task<ListResultDto<BlogDto>> GetListAsync()
    {
        return await RequestAsync<ListResultDto<BlogDto>>(nameof(GetListAsync));
    }

    public virtual async Task<BlogDto> GetAsync(Guid id)
    {
        return await RequestAsync<BlogDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task<BlogDto> CreateAsync(CreateBlogDto input)
    {
        return await RequestAsync<BlogDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(CreateBlogDto), input }
        });
    }

    public virtual async Task<BlogDto> UpdateAsync(Guid id, UpdateBlogDto input)
    {
        return await RequestAsync<BlogDto>(nameof(UpdateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id },
            { typeof(UpdateBlogDto), input }
        });
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await RequestAsync(nameof(DeleteAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }

    public virtual async Task ClearCacheAsync(Guid id)
    {
        await RequestAsync(nameof(ClearCacheAsync), new ClientProxyRequestTypeValue
        {
            { typeof(Guid), id }
        });
    }
}