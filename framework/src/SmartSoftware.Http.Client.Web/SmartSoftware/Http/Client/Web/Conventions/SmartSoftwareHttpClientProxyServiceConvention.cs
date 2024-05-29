using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSoftware.Application.Services;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.Conventions;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client.ClientProxying;
using SmartSoftware.Http.Client.StaticProxying;
using SmartSoftware.Http.Modeling;
using SmartSoftware.Reflection;

namespace SmartSoftware.Http.Client.Web.Conventions;

[DisableConventionalRegistration]
public class SmartSoftwareHttpClientProxyServiceConvention : SmartSoftwareServiceConvention
{
    protected readonly IClientProxyApiDescriptionFinder ClientProxyApiDescriptionFinder;
    protected readonly List<ControllerModel> ControllerWithAttributeRoute;
    protected readonly List<ActionModel> ActionWithAttributeRoute;
    protected readonly SmartSoftwareHttpClientStaticProxyingOptions StaticProxyingOptions;

    public SmartSoftwareHttpClientProxyServiceConvention(
        IOptions<SmartSoftwareAspNetCoreMvcOptions> options,
        IConventionalRouteBuilder conventionalRouteBuilder,
        IClientProxyApiDescriptionFinder clientProxyApiDescriptionFinder,
        IOptions<SmartSoftwareHttpClientStaticProxyingOptions> staticProxyingOptions)
        : base(options, conventionalRouteBuilder)
    {
        ClientProxyApiDescriptionFinder = clientProxyApiDescriptionFinder;
        StaticProxyingOptions = staticProxyingOptions.Value;
        ControllerWithAttributeRoute = new List<ControllerModel>();
        ActionWithAttributeRoute = new List<ActionModel>();
    }

    protected override IList<ControllerModel> GetControllers(ApplicationModel application)
    {
        return application.Controllers.Where(c => !SmartSoftwareHttpClientProxyHelper.IsClientProxyService(c.ControllerType)).ToList();
    }

    protected virtual IList<ControllerModel> GetClientProxyControllers(ApplicationModel application)
    {
        return application.Controllers.Where(c => SmartSoftwareHttpClientProxyHelper.IsClientProxyService(c.ControllerType)).ToList();
    }

    protected override void ApplyForControllers(ApplicationModel application)
    {
        base.ApplyForControllers(application);

        foreach (var controller in GetClientProxyControllers(application))
        {
            if (ShouldBeRemove(application, controller))
            {
                application.Controllers.Remove(controller);
                continue;
            }

            controller.ControllerName = controller.ControllerName.RemovePostFix("ClientProxy");

            var controllerApiDescription = FindControllerApiDescriptionModel(controller);
            if (controllerApiDescription != null &&
                !controllerApiDescription.ControllerGroupName.IsNullOrWhiteSpace())
            {
                controller.ControllerName = controllerApiDescription.ControllerGroupName!;
            }

            ConfigureClientProxySelector(controller);
            ConfigureClientProxyApiExplorer(controller);
            ConfigureParameters(controller);
        }
    }

    protected override void ConfigureParameters(ControllerModel controller)
    {
        foreach (var action in controller.Actions)
        {
            foreach (var prm in action.Parameters)
            {
                if (prm.BindingInfo != null)
                {
                    continue;
                }

                if (StaticProxyingOptions.BindingFromQueryTypes.Contains(prm.ParameterInfo.ParameterType))
                {
                    prm.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromQueryAttribute() });
                }
            }
        }

        base.ConfigureParameters(controller);
    }

    protected virtual bool ShouldBeRemove(ApplicationModel application, ControllerModel controllerModel)
    {
        return application.Controllers
            .Where(x => x.ControllerType != controllerModel.ControllerType)
            .Any(x => FindAppServiceInterfaceType(x) == FindAppServiceInterfaceType(controllerModel));
    }

    protected virtual void ConfigureClientProxySelector(ControllerModel controller)
    {
        RemoveEmptySelectors(controller.Selectors);

        var moduleApiDescription = FindModuleApiDescriptionModel(controller);
        if (moduleApiDescription != null && !moduleApiDescription.RootPath.IsNullOrWhiteSpace())
        {
            var selector = controller.Selectors.FirstOrDefault();
            selector?.EndpointMetadata.Add(new AreaAttribute(moduleApiDescription.RootPath));
            controller.RouteValues.Add(new KeyValuePair<string, string?>("area", moduleApiDescription.RootPath));
        }

        var controllerType = controller.ControllerType.AsType();
        var remoteServiceAtt = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(controllerType.GetTypeInfo());
        if (remoteServiceAtt != null && !remoteServiceAtt.IsEnabledFor(controllerType))
        {
            return;
        }

        if (controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
        {
            return;
        }

        foreach (var action in controller.Actions)
        {
            ConfigureClientProxySelector(controller, action);
        }
    }

    protected virtual void ConfigureClientProxySelector(ControllerModel controller, ActionModel action)
    {
        try
        {
            RemoveEmptySelectors(action.Selectors);

            var remoteServiceAtt = ReflectionHelper.GetSingleAttributeOrDefault<RemoteServiceAttribute>(action.ActionMethod);
            if (remoteServiceAtt != null && !remoteServiceAtt.IsEnabledFor(action.ActionMethod))
            {
                return;
            }

            var actionApiDescriptionModel = FindActionApiDescriptionModel(controller, action);
            if (actionApiDescriptionModel == null)
            {
                Logger.LogWarning($"Could not find ApiDescriptionModel for action: {action.ActionName} in controller: {controller.ControllerName}, May be the generate-proxy.json is not up to date.");
                return;;
            }

            ControllerWithAttributeRoute.Add(controller);
            ActionWithAttributeRoute.Add(action);

            if (!action.Selectors.Any())
            {
                var ssServiceSelectorModel = new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(template: actionApiDescriptionModel.Url)),
                    ActionConstraints = { new HttpMethodActionConstraint(new[] { actionApiDescriptionModel.HttpMethod! }) }
                };

                action.Selectors.Add(ssServiceSelectorModel);
            }
            else
            {
                foreach (var selector in action.Selectors)
                {
                    var httpMethod = selector.ActionConstraints
                        .OfType<HttpMethodActionConstraint>()
                        .FirstOrDefault()?
                        .HttpMethods?
                        .FirstOrDefault();

                    if (httpMethod == null)
                    {
                        httpMethod = actionApiDescriptionModel.HttpMethod;
                    }

                    if (selector.AttributeRouteModel == null)
                    {
                        selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(template: actionApiDescriptionModel.Url));
                    }

                    if (!selector.ActionConstraints.OfType<HttpMethodActionConstraint>().Any())
                    {
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { httpMethod! }));
                    }
                }
            }
        }
        catch
        {
            Logger.LogError($"An exception has occured while configuring client proxy selector. Controller: {controller.ControllerName} ({controller.ControllerType.AssemblyQualifiedName}), Action: {action.ActionName}");
            throw;
        }
    }

    protected virtual void ConfigureClientProxyApiExplorer(ControllerModel controller)
    {
        if (ControllerWithAttributeRoute.Contains(controller))
        {
            if (Options.ChangeControllerModelApiExplorerGroupName && controller.ApiExplorer.GroupName.IsNullOrEmpty())
            {
                controller.ApiExplorer.GroupName = controller.ControllerName;
            }

            if (controller.ApiExplorer.IsVisible == null)
            {
                controller.ApiExplorer.IsVisible = IsVisibleRemoteService(controller.ControllerType);
            }
        }

        foreach (var action in controller.Actions)
        {
            if (ActionWithAttributeRoute.Contains(action))
            {
                ConfigureApiExplorer(action);
            }
        }
    }

    protected virtual ModuleApiDescriptionModel? FindModuleApiDescriptionModel(ControllerModel controller)
    {
        var appServiceType = FindAppServiceInterfaceType(controller);
        if (appServiceType == null)
        {
            return null;
        }

        var applicationApiDescriptionModel = ClientProxyApiDescriptionFinder.GetApiDescription();
        foreach (var moduleApiDescription in applicationApiDescriptionModel.Modules.Values)
        {
            if (moduleApiDescription.Controllers.Values.Any(x => x.Interfaces.Any(t => t.Type == appServiceType.FullName)))
            {
                return moduleApiDescription;
            }
        }

        return null;
    }

    protected virtual ControllerApiDescriptionModel? FindControllerApiDescriptionModel(ControllerModel controller)
    {
        var appServiceType = FindAppServiceInterfaceType(controller);
        if (appServiceType == null)
        {
            return null;
        }

        var applicationApiDescriptionModel = ClientProxyApiDescriptionFinder.GetApiDescription();
        foreach (var controllerApiDescription in applicationApiDescriptionModel.Modules.Values.SelectMany(x => x.Controllers.Values))
        {
            if (controllerApiDescription.Interfaces.Any(t => t.Type == appServiceType.FullName))
            {
                return controllerApiDescription;
            }
        }

        return null;
    }

    protected virtual ActionApiDescriptionModel? FindActionApiDescriptionModel(ControllerModel controller, ActionModel action)
    {
        var appServiceType = FindAppServiceInterfaceType(controller);
        if (appServiceType == null)
        {
            return null;
        }

        var key =
            $"{appServiceType.FullName}." +
            $"{action.ActionMethod.Name}." +
            $"{string.Join("-", action.Parameters.Select(x => TypeHelper.GetFullNameHandlingNullableAndGenerics(x.ParameterType)))}";

        var actionApiDescriptionModel = ClientProxyApiDescriptionFinder.FindAction(key);
        if (actionApiDescriptionModel == null)
        {
            return null;
        }

        if (actionApiDescriptionModel.ImplementFrom!.StartsWith("SmartSoftware.Application.Services"))
        {
            return actionApiDescriptionModel;
        }

        if (appServiceType.FullName != null && actionApiDescriptionModel.ImplementFrom.StartsWith(appServiceType.FullName))
        {
            return actionApiDescriptionModel;
        }

        return null;
    }

    protected virtual Type? FindAppServiceInterfaceType(ControllerModel controller)
    {
        return controller.ControllerType.GetInterfaces()
            .FirstOrDefault(type => !type.IsGenericType &&
                                    type != typeof(IApplicationService) &&
                                    typeof(IApplicationService).IsAssignableFrom(type));
    }
}
