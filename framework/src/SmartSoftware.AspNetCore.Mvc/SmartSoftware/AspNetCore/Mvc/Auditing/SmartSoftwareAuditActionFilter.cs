using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using SmartSoftware.Aspects;
using SmartSoftware.AspNetCore.Filters;
using SmartSoftware.Auditing;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.Auditing;

public class SmartSoftwareAuditActionFilter : IAsyncActionFilter, ISmartSoftwareFilter, ITransientDependency
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!ShouldSaveAudit(context, out var auditLog, out var auditLogAction))
        {
            await next();
            return;
        }

        using (SmartSoftwareCrossCuttingConcerns.Applying(context.Controller, SmartSoftwareCrossCuttingConcerns.Auditing))
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var result = await next();

                if (result.Exception != null && !auditLog!.Exceptions.Contains(result.Exception))
                {
                    auditLog!.Exceptions.Add(result.Exception);
                }
            }
            catch (Exception ex)
            {
                if (!auditLog!.Exceptions.Contains(ex))
                {
                    auditLog!.Exceptions.Add(ex);
                }
                throw;
            }
            finally
            {
                stopwatch.Stop();

                if (auditLogAction != null)
                {
                    auditLogAction.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                    auditLog!.Actions.Add(auditLogAction);
                }
            }
        }
    }

    private bool ShouldSaveAudit(ActionExecutingContext context, out AuditLogInfo? auditLog, out AuditLogActionInfo? auditLogAction)
    {
        auditLog = null;
        auditLogAction = null;

        var options = context.GetRequiredService<IOptions<SmartSoftwareAuditingOptions>>().Value;
        if (!options.IsEnabled)
        {
            return false;
        }

        if (!context.ActionDescriptor.IsControllerAction())
        {
            return false;
        }

        var auditLogScope = context.GetRequiredService<IAuditingManager>().Current;
        if (auditLogScope == null)
        {
            return false;
        }

        var auditingHelper = context.GetRequiredService<IAuditingHelper>();
        if (!auditingHelper.ShouldSaveAudit(
                context.ActionDescriptor.GetMethodInfo(),
                defaultValue: GetDefaultAuditBehavior(options, context.ActionDescriptor)))
        {
            return false;
        }

        auditLog = auditLogScope.Log;

        if (!options.DisableLogActionInfo)
        {
            auditLogAction = auditingHelper.CreateAuditLogAction(
                auditLog,
                context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType(),
                context.ActionDescriptor.AsControllerActionDescriptor().MethodInfo,
                context.ActionArguments
            );
        }

        return true;
    }

    private static bool GetDefaultAuditBehavior(
        SmartSoftwareAuditingOptions ssAuditingOptions,
        ActionDescriptor actionDescriptor)
    {
        if (!ssAuditingOptions.IsEnabledForIntegrationServices &&
            IntegrationServiceAttribute.IsDefinedOrInherited(
                actionDescriptor
                    .AsControllerActionDescriptor()
                    .ControllerTypeInfo)
            )
        {
            return false;
        }

        return true;
    }
}
