﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Filters;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Reflection;
using SmartSoftware.Validation;

namespace SmartSoftware.AspNetCore.Mvc.Validation;

public class SmartSoftwareValidationActionFilter : IAsyncActionFilter, ISmartSoftwareFilter, ITransientDependency
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionDescriptor.IsControllerAction() ||
            !context.ActionDescriptor.HasObjectResult())
        {
            await next();
            return;
        }

        if (!context.GetRequiredService<IOptions<SmartSoftwareAspNetCoreMvcOptions>>().Value.AutoModelValidation)
        {
            await next();
            return;
        }

        if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableValidationAttribute>(context.ActionDescriptor.GetMethodInfo()) != null)
        {
            await next();
            return;
        }

        if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableValidationAttribute>(context.Controller.GetType()) != null)
        {
            await next();
            return;
        }

        if (context.ActionDescriptor.GetMethodInfo().DeclaringType != context.Controller.GetType())
        {
            var baseMethod = context.ActionDescriptor.GetMethodInfo();

            var overrideMethod = context.Controller.GetType().GetMethods().FirstOrDefault(x =>
                x.DeclaringType == context.Controller.GetType() &&
                x.Name == baseMethod.Name &&
                x.ReturnType == baseMethod.ReturnType &&
                x.GetParameters().Select(p => p.ToString()).SequenceEqual(baseMethod.GetParameters().Select(p => p.ToString())));

            if (overrideMethod != null)
            {
                if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableValidationAttribute>(overrideMethod) != null)
                {
                    await next();
                    return;
                }
            }
        }

        context.GetRequiredService<IModelStateValidator>().Validate(context.ModelState);
        await next();
    }
}
