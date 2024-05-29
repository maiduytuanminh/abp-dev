// This file is automatically generated by SS framework to use MVC Controllers from CSharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Content;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Http.Client.ClientProxying;
using SmartSoftware.Http.Modeling;
using SmartSoftware.Blogging.Files;

// ReSharper disable once CheckNamespace
namespace SmartSoftware.Blogging;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IFileAppService), typeof(BlogFilesClientProxy))]
public partial class BlogFilesClientProxy : ClientProxyBase<IFileAppService>, IFileAppService
{
    public virtual async Task<RawFileDto> GetAsync(string name)
    {
        return await RequestAsync<RawFileDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), name }
        });
    }

    public virtual async Task<IRemoteStreamContent> GetFileAsync(string name)
    {
        return await RequestAsync<IRemoteStreamContent>(nameof(GetFileAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), name }
        });
    }

    public virtual async Task<FileUploadOutputDto> CreateAsync(FileUploadInputDto input)
    {
        return await RequestAsync<FileUploadOutputDto>(nameof(CreateAsync), new ClientProxyRequestTypeValue
        {
            { typeof(FileUploadInputDto), input }
        });
    }
}
