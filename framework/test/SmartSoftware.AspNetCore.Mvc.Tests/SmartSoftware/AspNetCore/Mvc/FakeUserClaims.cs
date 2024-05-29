using System.Collections.Generic;
using System.Security.Claims;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc;

public class FakeUserClaims : ISingletonDependency
{
    public List<Claim> Claims { get; } = new List<Claim>();
}
