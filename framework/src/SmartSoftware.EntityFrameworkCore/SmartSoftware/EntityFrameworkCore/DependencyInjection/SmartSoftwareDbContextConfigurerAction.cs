using System;
using JetBrains.Annotations;

namespace SmartSoftware.EntityFrameworkCore.DependencyInjection;

public class SmartSoftwareDbContextConfigurerAction : ISmartSoftwareDbContextConfigurer
{
    [NotNull]
    public Action<SmartSoftwareDbContextConfigurationContext> Action { get; }

    public SmartSoftwareDbContextConfigurerAction([NotNull] Action<SmartSoftwareDbContextConfigurationContext> action)
    {
        Check.NotNull(action, nameof(action));

        Action = action;
    }

    public void Configure(SmartSoftwareDbContextConfigurationContext context)
    {
        Action.Invoke(context);
    }
}

public class SmartSoftwareDbContextConfigurerAction<TDbContext> : SmartSoftwareDbContextConfigurerAction
    where TDbContext : SmartSoftwareDbContext<TDbContext>
{
    public SmartSoftwareDbContextConfigurerAction([NotNull] Action<SmartSoftwareDbContextConfigurationContext> action)
        : base(action)
    {
    }
}
