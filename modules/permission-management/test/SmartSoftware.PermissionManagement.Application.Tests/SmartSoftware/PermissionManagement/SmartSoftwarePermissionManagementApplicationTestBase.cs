using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SmartSoftware.Users;

namespace SmartSoftware.PermissionManagement;

public class SmartSoftwarePermissionManagementApplicationTestBase : PermissionManagementTestBase<SmartSoftwarePermissionManagementApplicationTestModule>
{
    protected Guid? CurrentUserId { get; set; }

    protected SmartSoftwarePermissionManagementApplicationTestBase()
    {
        CurrentUserId = Guid.NewGuid();
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        var currentUser = Substitute.For<ICurrentUser>();
        currentUser.Roles.Returns(new[] { "admin" });
        currentUser.IsAuthenticated.Returns(true);

        services.AddSingleton(currentUser);
    }
}
