﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Aspects;
using SmartSoftware.DependencyInjection;
using SmartSoftware.DynamicProxy;
using SmartSoftware.Uow;
using SmartSoftware.Users;

namespace SmartSoftware.Auditing;

public class AuditingInterceptor : SmartSoftwareInterceptor, ITransientDependency
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AuditingInterceptor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task InterceptAsync(ISmartSoftwareMethodInvocation invocation)
    {
        using (var serviceScope = _serviceScopeFactory.CreateScope())
        {
            var auditingHelper = serviceScope.ServiceProvider.GetRequiredService<IAuditingHelper>();
            var auditingOptions = serviceScope.ServiceProvider.GetRequiredService<IOptions<SmartSoftwareAuditingOptions>>().Value;

            if (!ShouldIntercept(invocation, auditingOptions, auditingHelper))
            {
                await invocation.ProceedAsync();
                return;
            }

            var auditingManager = serviceScope.ServiceProvider.GetRequiredService<IAuditingManager>();
            if (auditingManager.Current != null)
            {
                await ProceedByLoggingAsync(invocation, auditingOptions, auditingHelper, auditingManager.Current);
            }
            else
            {
                var currentUser = serviceScope.ServiceProvider.GetRequiredService<ICurrentUser>();
                var unitOfWorkManager = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
                await ProcessWithNewAuditingScopeAsync(invocation, auditingOptions, currentUser, auditingManager, auditingHelper, unitOfWorkManager);
            }
        }
    }

    protected virtual bool ShouldIntercept(ISmartSoftwareMethodInvocation invocation,
        SmartSoftwareAuditingOptions options,
        IAuditingHelper auditingHelper)
    {
        if (!options.IsEnabled)
        {
            return false;
        }

        if (SmartSoftwareCrossCuttingConcerns.IsApplied(invocation.TargetObject, SmartSoftwareCrossCuttingConcerns.Auditing))
        {
            return false;
        }

        if (!auditingHelper.ShouldSaveAudit(
                invocation.Method,
                ignoreIntegrationServiceAttribute: options.IsEnabledForIntegrationServices))
        {
            return false;
        }

        return true;
    }

    private static async Task ProceedByLoggingAsync(
        ISmartSoftwareMethodInvocation invocation,
        SmartSoftwareAuditingOptions options,
        IAuditingHelper auditingHelper,
        IAuditLogScope auditLogScope)
    {
        var auditLog = auditLogScope.Log;

        AuditLogActionInfo? auditLogAction = null;
        if (!options.DisableLogActionInfo)
        {
            auditLogAction = auditingHelper.CreateAuditLogAction(
                auditLog,
                invocation.TargetObject.GetType(),
                invocation.Method,
                invocation.Arguments
            );
        }

        var stopwatch = Stopwatch.StartNew();

        try
        {
            await invocation.ProceedAsync();
        }
        catch (Exception ex)
        {
            auditLog.Exceptions.Add(ex);
            throw;
        }
        finally
        {
            stopwatch.Stop();

            if (auditLogAction != null)
            {
                auditLogAction.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                auditLog.Actions.Add(auditLogAction);
            }
        }
    }

    private async Task ProcessWithNewAuditingScopeAsync(
        ISmartSoftwareMethodInvocation invocation,
        SmartSoftwareAuditingOptions options,
        ICurrentUser currentUser,
        IAuditingManager auditingManager,
        IAuditingHelper auditingHelper,
        IUnitOfWorkManager unitOfWorkManager)
    {
        var hasError = false;
        using (var saveHandle = auditingManager.BeginScope())
        {
            try
            {
                await ProceedByLoggingAsync(invocation, options, auditingHelper, auditingManager.Current!);

                Debug.Assert(auditingManager.Current != null);
                if (auditingManager.Current!.Log.Exceptions.Any())
                {
                    hasError = true;
                }
            }
            catch (Exception)
            {
                hasError = true;
                throw;
            }
            finally
            {
                if (await ShouldWriteAuditLogAsync(invocation, auditingManager.Current!.Log, options, currentUser, hasError))
                {
                    if (unitOfWorkManager.Current != null)
                    {
                        try
                        {
                            await unitOfWorkManager.Current.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            if (!auditingManager.Current.Log.Exceptions.Contains(ex))
                            {
                                auditingManager.Current.Log.Exceptions.Add(ex);
                            }
                        }
                    }

                    await saveHandle.SaveAsync();
                }
            }
        }
    }

    private async Task<bool> ShouldWriteAuditLogAsync(
        ISmartSoftwareMethodInvocation invocation,
        AuditLogInfo auditLogInfo,
        SmartSoftwareAuditingOptions options,
        ICurrentUser currentUser,
        bool hasError)
    {
        foreach (var selector in options.AlwaysLogSelectors)
        {
            if (await selector(auditLogInfo))
            {
                return true;
            }
        }

        if (options.AlwaysLogOnException && hasError)
        {
            return true;
        }

        if (!options.IsEnabledForAnonymousUsers && !currentUser.IsAuthenticated)
        {
            return false;
        }

        if (!options.IsEnabledForGetRequests &&
            invocation.Method.Name.StartsWith("Get", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return true;
    }
}
