﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Authorization.TestServices;

[Authorize]
public class MySimpleAuthorizedService : IMySimpleAuthorizedService, ITransientDependency
{
    public Task<int> ProtectedByClassAsync()
    {
        return Task.FromResult(42);
    }

    [AllowAnonymous]
    public Task<int> AnonymousAsync()
    {
        return Task.FromResult(42);
    }
}
