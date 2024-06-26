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
using SmartSoftware.Docs.Documents;

// ReSharper disable once CheckNamespace
namespace SmartSoftware.Docs.Documents;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IDocumentAppService), typeof(DocsDocumentClientProxy))]
public partial class DocsDocumentClientProxy : ClientProxyBase<IDocumentAppService>, IDocumentAppService
{
    public virtual async Task<DocumentWithDetailsDto> GetAsync(GetDocumentInput input)
    {
        return await RequestAsync<DocumentWithDetailsDto>(nameof(GetAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetDocumentInput), input }
        });
    }

    public virtual async Task<DocumentWithDetailsDto> GetDefaultAsync(GetDefaultDocumentInput input)
    {
        return await RequestAsync<DocumentWithDetailsDto>(nameof(GetDefaultAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetDefaultDocumentInput), input }
        });
    }

    public virtual async Task<NavigationNode> GetNavigationAsync(GetNavigationDocumentInput input)
    {
        return await RequestAsync<NavigationNode>(nameof(GetNavigationAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetNavigationDocumentInput), input }
        });
    }

    public virtual async Task<DocumentResourceDto> GetResourceAsync(GetDocumentResourceInput input)
    {
        return await RequestAsync<DocumentResourceDto>(nameof(GetResourceAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetDocumentResourceInput), input }
        });
    }

    public virtual async Task<PagedResultDto<DocumentSearchOutput>> SearchAsync(DocumentSearchInput input)
    {
        return await RequestAsync<PagedResultDto<DocumentSearchOutput>>(nameof(SearchAsync), new ClientProxyRequestTypeValue
        {
            { typeof(DocumentSearchInput), input }
        });
    }

    public virtual async Task<bool> FullSearchEnabledAsync()
    {
        return await RequestAsync<bool>(nameof(FullSearchEnabledAsync));
    }

    public virtual async Task<List<String>> GetUrlsAsync(string prefix)
    {
        return await RequestAsync<List<String>>(nameof(GetUrlsAsync), new ClientProxyRequestTypeValue
        {
            { typeof(string), prefix }
        });
    }

    public virtual async Task<DocumentParametersDto> GetParametersAsync(GetParametersDocumentInput input)
    {
        return await RequestAsync<DocumentParametersDto>(nameof(GetParametersAsync), new ClientProxyRequestTypeValue
        {
            { typeof(GetParametersDocumentInput), input }
        });
    }
}
