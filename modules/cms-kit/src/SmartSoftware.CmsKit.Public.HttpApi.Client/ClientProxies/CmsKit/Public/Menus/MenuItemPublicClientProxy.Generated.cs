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
using SmartSoftware.CmsKit.Menus;
using SmartSoftware.CmsKit.Public.Menus;

// ReSharper disable once CheckNamespace
namespace SmartSoftware.CmsKit.Public.Menus;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IMenuItemPublicAppService), typeof(MenuItemPublicClientProxy))]
public partial class MenuItemPublicClientProxy : ClientProxyBase<IMenuItemPublicAppService>, IMenuItemPublicAppService
{
    public virtual async Task<List<MenuItemDto>> GetListAsync()
    {
        return await RequestAsync<List<MenuItemDto>>(nameof(GetListAsync));
    }
}
